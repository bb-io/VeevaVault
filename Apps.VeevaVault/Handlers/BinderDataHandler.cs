using Apps.VeevaVault.Constants;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Helper;
using Apps.VeevaVault.Models.Entities.Document;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.VeevaVault.Handlers;

public class BinderDataHandler(InvocationContext invocationContext) 
    : VeevaVaultInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        string vql = new VqlBuilder("documents", VqlFields.MinimalDocument)
            .Find(context.SearchString, "name__v")
            .Where("binder__v = true")
            .Build();

        var response = await Client.Query<DocumentMinimalEntity>(vql, maxPages: 1);
        return response.Select(x => new DataSourceItem(x.Id.ToString(), x.Name)).ToList();
    }
}