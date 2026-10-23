using Apps.VeevaVault.Api;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Responses.Document.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.VeevaVault.Handlers;

public class DocumentVersionDataHandler : VeevaVaultInvocable, IAsyncDataSourceItemHandler
{
    private readonly string _documentId;
    
    public DocumentVersionDataHandler(
        InvocationContext invocationContext, 
        [ActionParameter] DocumentIdentifier documentIdentifier) : base(invocationContext)
    {
        if (string.IsNullOrEmpty(documentIdentifier.DocumentId))
            throw new PluginMisconfigurationException("Please specify a document ID first");

        _documentId = documentIdentifier.DocumentId;
    }

    // https://general.veevavault.dev/vault-api/api-reference/26.2/documents/retrieve-documents/retrieve-document-versions/
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        var request = new VeevaRequest($"objects/documents/{_documentId}/versions");
        var response = await Client.ExecuteWithErrorHandling<RetrieveDocumentVersionsResponse>(request);
        return response.Versions
            .Where(x => x.Number.MatchesSearch(context.SearchString))
            .Select(x => new DataSourceItem(x.Number, x.Number))
            .ToList();
    }
}