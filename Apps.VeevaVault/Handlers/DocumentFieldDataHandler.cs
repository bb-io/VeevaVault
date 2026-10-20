using Apps.VeevaVault.Api;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Responses.Document.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.VeevaVault.Handlers;

public class DocumentFieldDataHandler : VeevaVaultInvocable, IAsyncDataSourceItemHandler
{
    private readonly string _documentId;
    
    public DocumentFieldDataHandler(
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

        var typeRequest = new VeevaRequest($"metadata/objects/documents/types/{typeApiName}");
        var typeResponse = await Client.ExecuteWithErrorHandling<RetrieveAllDocumentFieldsResponse>(typeRequest);

        return typeResponse.Properties
            .Where(x => x.Editable && !x.Disabled && !x.Hidden)
            .Where(x => x.Label.MatchesSearch(context.SearchString) || x.Name.MatchesSearch(context.SearchString))
            .Select(x => new DataSourceItem(x.Name, x.Label ?? x.Name))
            .ToList();
    }
}