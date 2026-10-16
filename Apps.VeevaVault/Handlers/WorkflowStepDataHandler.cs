using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Helper;
using Apps.VeevaVault.Models.Entities.Workflow;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.VeevaVault.Handlers;

public class WorkflowStepDataHandler(InvocationContext invocationContext) 
    : VeevaVaultInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        string vql = new VqlBuilder("active_workflow_task__sys", ["name__sys", "label__sys"]).Build();
        var result = await Client.Query<WorkflowStepEntity>(vql, maxPages: 1);

        return result
            .DistinctBy(x => x.Name)
            .Where(x => x.Label.MatchesSearch(context.SearchString))
            .Select(x => new DataSourceItem(x.Name, x.Label))
            .ToList();
    }
}