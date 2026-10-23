using Apps.VeevaVault.Api;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Responses.Document.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.VeevaVault.Handlers;

public class DocumentRelationshipDataHandler : VeevaVaultInvocable, IAsyncDataSourceItemHandler
{
    private readonly string _documentId;

    public DocumentRelationshipDataHandler(
        InvocationContext invocationContext,
        [ActionParameter] DocumentIdentifier documentIdentifier) : base(invocationContext)
    {
        if (string.IsNullOrEmpty(documentIdentifier.DocumentId))
            throw new PluginMisconfigurationException("Please specify a document ID first");

        _documentId = documentIdentifier.DocumentId;
    }

    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        string typeApiName = await Client.GetDocumentTypeApiName(_documentId);
        
        var request = new VeevaRequest($"metadata/objects/documents/types/{typeApiName}/relationships");
        var response = await Client.ExecuteWithErrorHandling<RetrieveDocumentTypeRelationshipsResponse>(request);

        return response.RelationshipTypes
            .Where(x => x.Label.MatchesSearch(context.SearchString))
            .Select(x => new DataSourceItem(x.Value, x.Label))
            .ToList();
    }
}