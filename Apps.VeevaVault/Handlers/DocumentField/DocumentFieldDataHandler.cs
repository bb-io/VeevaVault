using Apps.VeevaVault.Extensions;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.VeevaVault.Handlers.DocumentField;

public class DocumentFieldDataHandler(InvocationContext invocationContext)
    : VeevaVaultInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        var fields = await Client.GetAllDocumentFields();

        return fields
            .Where(x => !x.Disabled)
            .Where(x => x.Label.MatchesSearch(context.SearchString) || x.Name.MatchesSearch(context.SearchString))
            .Select(x => new DataSourceItem(x.Name, x.Label ?? x.Name))
            .ToList();
    }
}