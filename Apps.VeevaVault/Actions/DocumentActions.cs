using Apps.VeevaVault.Constants;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Helper;
using Apps.VeevaVault.Models.Entities.Document;
using Apps.VeevaVault.Models.Requests.Document;
using Apps.VeevaVault.Models.Responses.Document;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.VeevaVault.Actions;

[ActionList("Documents")]
public class DocumentActions(InvocationContext invocationContext) : VeevaVaultInvocable(invocationContext)
{
    [Action("Search documents", Description = "Search documents with a specific criteria")]
    public async Task<SearchDocumentsResponse> SearchDocuments(
        [ActionParameter] SearchDocumentsRequest input)
    {
        List<long>? documentIds = null;
        if (!string.IsNullOrEmpty(input.BinderId))
        {
            documentIds = await Client.GetBinderDocumentIds(input.BinderId);
            if (documentIds.Count == 0)
                return new([]);
        }
        
        if (!string.IsNullOrEmpty(input.WorkflowStepName))
        {
            var stepIds = await Client.GetWorkflowStepDocumentIds(input.WorkflowStepName);
            if (stepIds.Count == 0)
                return new([]);

            documentIds = documentIds is null ? stepIds : documentIds.Intersect(stepIds).ToList();
            if (documentIds.Count == 0)
                return new([]);
        }
        
        string vql = new VqlBuilder("documents", VqlFields.Document)
            .Find(input.NameContains, "name__v")
            .Where("binder__v = false")
            .WhereContains("type__v", input.DocumentTypes)
            .WhereContains("id", documentIds)
            .Build();
        
        var response = await Client.Query<DocumentEntity>(vql);
        return new(response.Select(x => new DocumentResponse(x)).ToList());
    }
}