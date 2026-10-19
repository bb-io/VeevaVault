using Apps.VeevaVault.Api;
using Apps.VeevaVault.Constants;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Helper;
using Apps.VeevaVault.Models.Entities.Document;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Requests.Document;
using Apps.VeevaVault.Models.Responses.Document;
using Apps.VeevaVault.Models.Responses.Document.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

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
        
        if (!string.IsNullOrEmpty(input.WorkflowStep))
        {
            var stepIds = await Client.GetWorkflowStepDocumentIds(input.WorkflowStep);
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

    // https://general.veevavault.dev/vault-api/api-reference/26.2/documents/retrieve-documents/retrieve-document/
    [Action("Get document", Description = "Get details for a specific document")]
    public async Task<DocumentResponse> GetDocument([ActionParameter] DocumentIdentifier documentIdentifier)
    {
        var request = new VeevaRequest($"objects/documents/{documentIdentifier.DocumentId}");
        var response = await Client.ExecuteWithErrorHandling<RetrieveDocumentResponse>(request);
        return new(response.Document);
    }

    // https://general.veevavault.dev/vault-api/api-reference/26.2/documents/update-documents/update-single-document/
    [Action("Update document", Description = "Update fields of a specific document")]
    public Task UpdateDocument(
        [ActionParameter] DocumentIdentifier documentIdentifier,
        [ActionParameter] UpdateDocumentRequest input)
    {
        input.Validate();

        var request = new VeevaRequest($"objects/documents/{documentIdentifier.DocumentId}", Method.Put);
        foreach (var (name, value) in input.DocumentFieldNames.Zip(input.DocumentFieldValues))
            request.AddParameter(name, value);

        return Client.ExecuteWithErrorHandling(request);
    }
}