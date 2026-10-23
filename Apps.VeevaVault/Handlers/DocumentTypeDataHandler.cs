using Apps.VeevaVault.Api;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Helper;
using Apps.VeevaVault.Models.Responses.Document.Api;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.VeevaVault.Handlers;

public class DocumentTypeDataHandler(InvocationContext invocationContext) 
    : VeevaVaultInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    // https://general.veevavault.dev/vault-api/api-reference/26.2/documents/retrieve-document-types/retrieve-all-document-types/
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        var request = new VeevaRequest("metadata/objects/documents/types");
        var response = await Client.ExecuteWithErrorHandling<RetrieveAllDocumentTypesResponse>(request);
        return response.Types
            .Where(x => x.Label.MatchesSearch(context.SearchString))
            .Select(x => new DataSourceItem(ApiName.FromUrlValue(x.Value), x.Label))
            .ToList();
    }
}