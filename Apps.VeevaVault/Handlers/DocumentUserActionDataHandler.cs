using Apps.VeevaVault.Api;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Helper;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Responses.Document.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.VeevaVault.Handlers;

public class DocumentUserActionDataHandler : VeevaVaultInvocable, IAsyncDataSourceItemHandler
{
    private readonly string _documentId;
    private readonly string _documentVersionPath;
    
    public DocumentUserActionDataHandler(
        InvocationContext invocationContext, 
        [ActionParameter] DocumentIdentifier documentIdentifier,
        [ActionParameter] DocumentVersionIdentifier documentVersionIdentifier) : base(invocationContext)
    {
        if (string.IsNullOrEmpty(documentIdentifier.DocumentId))
            throw new PluginMisconfigurationException("Please specify a document ID first");
        
        if (string.IsNullOrEmpty(documentVersionIdentifier.DocumentVersion))
            throw new PluginMisconfigurationException("Please specify a document version first");

        _documentId = documentIdentifier.DocumentId;
        _documentVersionPath = DocumentVersionHelper.ToVersionPath(documentVersionIdentifier.DocumentVersion);
    }
    
    // https://general.veevavault.dev/vault-api/api-reference/26.2/document-lifecycle-workflows/document-user-actions/retrieve-document-user-actions/
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        var request = new VeevaRequest($"objects/documents/{_documentId}/{_documentVersionPath}/lifecycle_actions");
        var response = await Client.ExecuteWithErrorHandling<RetrieveDocumentUserActionsResponse>(request);
        return response.DocumentUserActions
            .Where(x => x.Label.MatchesSearch(context.SearchString))
            .Select(x => new DataSourceItem(x.Name, x.Label))
            .ToList();
    }
}