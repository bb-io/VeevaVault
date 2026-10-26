using Apps.VeevaVault.Constants;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Helper;
using Apps.VeevaVault.Models.Entities.User;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.VeevaVault.Handlers;

public class UserDataHandler(InvocationContext invocationContext) 
    : VeevaVaultInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        string vql = new VqlBuilder("user__sys", VqlFields.User)
            .Where("status__v = 'active__v'")
            .Find(context.SearchString, "name__v")
            .Build();

        var response = await Client.Query<UserEntity>(vql, maxPages: 1);
        return response.Select(x => new DataSourceItem(x.Id, $"{x.Name} ({x.Email})")).ToList();
    }
}