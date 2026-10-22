using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Helper;
using Apps.VeevaVault.Models.Entities.Language;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.VeevaVault.Handlers;

public class LanguageDataHandler(InvocationContext invocationContext) 
    : VeevaVaultInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        string vql = new VqlBuilder("language__sys", ["name__v", "admin_key__sys"])
            .Find(context.SearchString, "name__v")
            .Build();
        var response = await Client.Query<LanguageEntity>(vql, maxPages: 1);
        return response.Select(x => new DataSourceItem(x.AdminKey, x.Name)).ToList();
    }
}