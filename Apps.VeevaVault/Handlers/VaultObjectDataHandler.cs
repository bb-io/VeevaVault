using Apps.VeevaVault.Api;
using Apps.VeevaVault.Models.Responses.VaultObject.Api;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.VeevaVault.Handlers;

public class VaultObjectDataHandler(InvocationContext invocationContext) 
    : VeevaVaultInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    // https://general.veevavault.dev/vault-api/api-reference/26.2/vault-objects/retrieve-object-collection/
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        // Does not support pagination, just returns all objects in one response
        var request = new VeevaRequest("metadata/vobjects");
        var response = await Client.ExecuteWithErrorHandling<RetrieveVaultObjectsResponse>(request);

        return response.Objects
            .Where(x => 
                string.IsNullOrWhiteSpace(context.SearchString) || 
                x.Label.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Select(x => new DataSourceItem(x.Name, x.Label))
            .ToList();
    }
}
