using Apps.VeevaVault.Api;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Models.Entities.Job;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Requests.BulkTranslation;
using Apps.VeevaVault.Models.Responses;
using Apps.VeevaVault.Models.Responses.BulkTranslation.Api;
using Apps.VeevaVault.Models.Utility.Wrapper;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.VeevaVault.Actions;

[ActionList("Bulk translations")]
public class BulkTranslationActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) 
    : VeevaVaultInvocable(invocationContext)
{
    // https://general.veevavault.dev/vault-api/api-reference/26.2/bulk-translation/export-bulk-translation-file
    // https://general.veevavault.dev/vault-api/api-reference/26.2/jobs/retrieve-job-status
    [Action("Export bulk translation file", Description = "Export a CSV bulk translation file")]
    public async Task<FileResponse> ExportBulkTranslationFile(
        [ActionParameter] LanguageIdentifier languageIdentifier,
        [ActionParameter] ExportBulkTranslationFileRequest input)
    {
        string endpoint = $"messages/{input.MessageType}/language/{languageIdentifier.LanguageKey}/actions/export";
        var startRequest = new VeevaRequest(endpoint, Method.Post);
        var startResponse = await Client.ExecuteWithErrorHandling<DataWrapper<ExportBulkTranslationFileApiResponse>>(startRequest);
        
        string startedJobId = startResponse.Data.JobId;
        var statusRequest = new VeevaRequest($"services/jobs/{startedJobId}");
        
        var deadline = DateTime.UtcNow + TimeSpan.FromMinutes(19);
        JobStatusEntity job;
        while (true)
        {
            await Task.Delay(TimeSpan.FromSeconds(10));

            var jobData = await Client.ExecuteWithErrorHandling<DataWrapper<JobStatusEntity>>(statusRequest);
            if (jobData.Data.Status is "SUCCESS")
            {
                job = jobData.Data;
                break;
            }

            if (jobData.Data.Status is "ERRORS_ENCOUNTERED" or "CANCELLED" or "MISSING_FILES")
                throw new PluginApplicationException($"Vault job {startedJobId} finished with status {jobData.Data.Status}");

            if (DateTime.UtcNow > deadline)
                throw new PluginApplicationException("Job status check timeout exceeded");
        }

        var contentLink = job.Links.FirstOrDefault(x => x.Rel.Equals("content", StringComparison.OrdinalIgnoreCase)) ?? 
                          throw new PluginApplicationException($"Vault job {startedJobId} did not return a downloadable file");

        var downloadContentRequest = Client.CreateRequestFromHref(contentLink.Href);
        var file = await Client.DownloadFile(downloadContentRequest, fileManagementClient);
        return new(file);
    }
}