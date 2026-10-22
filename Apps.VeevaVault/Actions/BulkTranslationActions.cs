using Apps.VeevaVault.Api;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Requests.BulkTranslation;
using Apps.VeevaVault.Models.Responses;
using Apps.VeevaVault.Models.Responses.BulkTranslation;
using Apps.VeevaVault.Models.Responses.BulkTranslation.Api;
using Apps.VeevaVault.Models.Utility.Wrapper;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using RestSharp;

namespace Apps.VeevaVault.Actions;

[ActionList("Bulk translations")]
public class BulkTranslationActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) 
    : VeevaVaultInvocable(invocationContext)
{
    // https://general.veevavault.dev/vault-api/api-reference/26.2/bulk-translation/export-bulk-translation-file
    [Action("Export bulk translations file", Description = "Export a CSV bulk translations file")]
    public async Task<FileResponse> ExportBulkTranslationsFile(
        [ActionParameter] LanguageIdentifier languageIdentifier,
        [ActionParameter] MessageTypeIdentifier messageTypeIdentifier)
    {
        string endpoint = $"messages/{messageTypeIdentifier.MessageType}/language/{languageIdentifier.LanguageKey}/actions/export";
        var startRequest = new VeevaRequest(endpoint, Method.Post);
        var startResponse = await Client.ExecuteWithErrorHandling<DataWrapper<JobIdResponse>>(startRequest);
        
        string startedJobId = startResponse.Data.JobId;
        var job = await Client.WaitForJob(startedJobId);
        
        var contentLink = job.Links.FirstOrDefault(x => x.Rel.Equals("content", StringComparison.OrdinalIgnoreCase)) ?? 
                          throw new PluginApplicationException($"Vault job {startedJobId} did not return a downloadable file");

        var downloadContentRequest = Client.CreateRequestFromHref(contentLink.Href);
        var file = await Client.DownloadFile(downloadContentRequest, fileManagementClient);
        return new(file);
    }

    // https://general.veevavault.dev/vault-api/api-reference/26.2/bulk-translation/import-bulk-translation-file
    [Action("Import bulk translations file", Description = "Import a bulk translations file")]
    public async Task<ImportBulkTranslationsFileResponse> ImportBulkTranslationsFile(
        [ActionParameter] MessageTypeIdentifier messageTypeIdentifier,
        [ActionParameter] ImportBulkTranslationsFileRequest input)
    {
        await using var file = await fileManagementClient.DownloadAsync(input.File);
        var bytes = await file.GetByteData();
        string uploadedFilePath = await Client.UploadToFileStaging(bytes, input.File.Name);
        
        var importRequest = new VeevaRequest($"messages/{messageTypeIdentifier.MessageType}/actions/import", Method.Post)
            .AddParameter("file_paths", uploadedFilePath);
        var importResponse = await Client.ExecuteWithErrorHandling<DataWrapper<JobIdResponse>>(importRequest);
        
        string startedJobId = importResponse.Data.JobId;
        await Client.WaitForJob(startedJobId);

        var jobSummaryRequest = new VeevaRequest($"services/jobs/{startedJobId}/summary");
        var jobSummaryResponse = await Client.ExecuteWithErrorHandling<DataWrapper<RetrieveImportJobSummaryResponse>>(jobSummaryRequest);
        
        var errorsRequest = new VeevaRequest($"services/jobs/{startedJobId}/errors").AddHeader("Accept", "text/csv");
        var errorsResponse = await Client.ExecuteWithErrorHandling(errorsRequest);

        var errors = errorsResponse.Content?.ParseJobErrors();
        return errors?.Count > 0 
            ? throw new PluginApplicationException($"Import failed with errors: {string.Join("; ", errors)}") 
            : new(jobSummaryResponse.Data);
    }
}