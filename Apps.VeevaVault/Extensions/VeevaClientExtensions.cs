using System.Net.Mime;
using Apps.VeevaVault.Api;
using Apps.VeevaVault.Helper;
using Apps.VeevaVault.Models.Entities.Common;
using Apps.VeevaVault.Models.Entities.Document;
using Apps.VeevaVault.Models.Entities.File;
using Apps.VeevaVault.Models.Entities.Job;
using Apps.VeevaVault.Models.Entities.VaultObject;
using Apps.VeevaVault.Models.Entities.Workflow;
using Apps.VeevaVault.Models.Responses.Document.Api;
using Apps.VeevaVault.Models.Utility.Wrapper;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.VeevaVault.Extensions;

public static class VeevaClientExtensions
{
    // https://general.veevavault.dev/vault-api/api-reference/26.2/workflows/workflow-tasks/retrieve-workflow-task-action-details
    public static async Task<List<ActionVerdict>> GetTaskVerdicts(this VeevaClient client, string taskId)
    {
        string action = await client.GetCompleteActionName(taskId);
        var request = new VeevaRequest($"objects/objectworkflows/tasks/{taskId}/actions/{action}");
        var response = await client.ExecuteWithErrorHandling<DataWrapper<WorkflowTaskActionEntity>>(request);

        return response.Data.Controls.SelectMany(x => x.Verdicts).ToList();
    }
    
    // https://general.veevavault.dev/vault-api/api-reference/26.2/workflows/workflow-tasks/retrieve-workflow-task-actions/
    public static async Task<string> GetCompleteActionName(this VeevaClient client, string taskId)
    {
        var request = new VeevaRequest($"objects/objectworkflows/tasks/{taskId}/actions");
        var response = await client.ExecuteWithErrorHandling<DataArrayWrapper<WorkflowTaskActionNameEntity>>(request);

        return response.Data.Select(x => x.Name).FirstOrDefault(x => x is "complete" or "mdwcomplete") ?? 
               throw new PluginMisconfigurationException(
                   $"Task {taskId} can't be completed by the connected user. It's not assigned to them or is already completed.");
    }
    
    // https://general.veevavault.dev/vault-api/api-reference/26.2/workflows/workflow-tasks/retrieve-workflow-task-details/
    public static async Task<WorkflowTaskEntity> GetWorkflowTask(this VeevaClient client, string taskId)
    {
        var request = new VeevaRequest($"objects/objectworkflows/tasks/{taskId}");
        var response = await client.ExecuteWithErrorHandling<DataArrayWrapper<WorkflowTaskEntity>>(request);
        return response.Data.First();
    }
    
    // https://general.veevavault.dev/vault-api/api-reference/26.2/jobs/retrieve-job-status
    public static async Task<JobStatusEntity> WaitForJob(this VeevaClient client, string startedJobId)
    {
        var deadline = DateTime.UtcNow + TimeSpan.FromMinutes(19);
        
        var statusRequest = new VeevaRequest($"services/jobs/{startedJobId}");
        while (true)
        {
            await Task.Delay(TimeSpan.FromSeconds(10));

            var jobData = await client.ExecuteWithErrorHandling<DataWrapper<JobStatusEntity>>(statusRequest);
            if (jobData.Data.Status is "SUCCESS")
                return jobData.Data;

            if (jobData.Data.Status is "ERRORS_ENCOUNTERED" or "CANCELLED" or "MISSING_FILES")
                throw new PluginApplicationException($"Vault job {startedJobId} finished with status {jobData.Data.Status}");

            if (DateTime.UtcNow > deadline)
                throw new PluginApplicationException("Job status check timeout exceeded");
        }
    }
    
    public static async Task<string> UploadToFileStaging(
        this VeevaClient client,
        byte[] fileBytes,
        string fileName)
    {
        string path = $"/{Guid.NewGuid()}_{fileName}";

        var request = new VeevaRequest("services/file_staging/items", Method.Post) { AlwaysMultipartFormData = true }
            .AddParameter("kind", "file")
            .AddParameter("path", path)
            .AddParameter("overwrite", "true")
            .AddFile("file", fileBytes, fileName);

        var response = await client.ExecuteWithErrorHandling<DataWrapper<FileStagingItemEntity>>(request);
        return response.Data.Path;
    }
    
    public static async Task<FileReference> DownloadFile(
        this VeevaClient client,
        RestRequest request,
        IFileManagementClient fileManagementClient)
    {
        var response = await client.ExecuteWithErrorHandling(request);

        string contentType = response.ContentType ?? MediaTypeNames.Application.Octet;
        string fileName = response.GetFilenameFromDisposition();
        var bytes = response.RawBytes ?? throw new PluginApplicationException("Vault returned 0 bytes");

        using var stream = new MemoryStream(bytes);
        return await fileManagementClient.UploadAsync(stream, contentType, fileName);
    }
    
    public static async Task<string> GetDocumentTypeApiName(this VeevaClient client, string documentId)
    {
        string vql = new VqlBuilder("documents", ["TONAME(type__v)"])
            .Where($"id = {long.Parse(documentId)}")
            .Build();

        var result = await client.Query<DocumentTypeNameEntity>(vql);
        return result.FirstOrDefault()?.Type ?? throw new PluginApplicationException($"Document '{documentId}' was not found");
    }
    
    public static async Task<DocumentEntity> GetDocument(this VeevaClient client, string documentId)
    {
        var request = new VeevaRequest($"objects/documents/{documentId}");
        var response = await client.ExecuteWithErrorHandling<RetrieveDocumentResponse>(request);
        return response.Document;
    }
    
    public static async Task<VaultObjectRecordEntity> GetObjectRecord(
        this VeevaClient client,
        string objectName,
        string recordId)
    {
        var request = new VeevaRequest($"vobjects/{objectName}/{recordId}");
        var response = await client.ExecuteWithErrorHandling<DataWrapper<VaultObjectRecordEntity>>(request);
        return response.Data;
    }
    
    public static async Task<List<long>> GetBinderDocumentIds(this VeevaClient client, string binderId)
    {
        if (!long.TryParse(binderId, out long parsedBinderId))
            throw new PluginMisconfigurationException($"Binder ID '{binderId}' is not a valid number");

        string vql = new VqlBuilder("binder_node__sys", ["content_id__sys"])
            .Where($"parent_binder_id__sys = {parsedBinderId}")
            .WhereEquals("type__sys", "document__sys")
            .Build();

        var nodes = await client.Query<BinderNodeEntity>(vql);
        return nodes.Select(x => x.ContentId).Distinct().ToList();
    }
    
    public static async Task<List<long>> GetWorkflowStepDocumentIds(this VeevaClient client, string stepName)
    {
        string tasksVql = new VqlBuilder("active_workflow_task__sys", ["id"])
            .WhereEquals("name__sys", stepName)
            .Build();

        var tasks = await client.Query<IdEntity>(tasksVql);
        if (tasks.Count == 0)
            return [];

        string itemsVql = new VqlBuilder("active_workflow_task_item__sys", ["document_id__sys"])
            .WhereContains("task__sys", tasks.Select(x => x.Id))
            .Build();

        var items = await client.Query<WorkflowTaskItemEntity>(itemsVql);
        return items.Select(x => x.DocumentId).Distinct().ToList();
    }

    public static Task<List<T>> Query<T>(this VeevaClient client, string vql, int? maxPages = null)
    {
        var request = new VeevaRequest("query", Method.Post).AddQueryParameter("q", vql);
        return client.PaginateCursor<T>(request, maxPages);
    }
}