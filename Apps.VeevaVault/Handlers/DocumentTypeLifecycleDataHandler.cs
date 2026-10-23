using Apps.VeevaVault.Api;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Responses.Document.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.VeevaVault.Handlers;

public class DocumentTypeLifecycleDataHandler : VeevaVaultInvocable, IAsyncDataSourceItemHandler
{
    private readonly string _documentType;
    
    public DocumentTypeLifecycleDataHandler(
        InvocationContext invocationContext, 
        [ActionParameter] DocumentTypeIdentifier documentTypeIdentifier) : base(invocationContext)
    {
        if (string.IsNullOrEmpty(documentTypeIdentifier.DocumentType))
            throw new PluginMisconfigurationException("Please specify a document type first");

        _documentType = documentTypeIdentifier.DocumentType;
    }

    // https://general.veevavault.dev/vault-api/api-reference/26.2/documents/retrieve-document-types/retrieve-document-type/
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        var request = new VeevaRequest($"metadata/objects/documents/types/{_documentType}");
        var response = await Client.ExecuteWithErrorHandling<RetrieveDocumentTypeResponse>(request);
        
        return response.AvailableLifecycles
            .Where(x => x.Label.MatchesSearch(context.SearchString))
            .Select(x => new DataSourceItem(x.Name, x.Label))
            .ToList();
    }
}