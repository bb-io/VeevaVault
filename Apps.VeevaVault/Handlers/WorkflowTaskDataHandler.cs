using Apps.VeevaVault.Api;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Models.Entities.Workflow;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.VeevaVault.Handlers;

public class WorkflowTaskDataHandler(InvocationContext invocationContext) 
    : VeevaVaultInvocable(invocationContext), IAsyncDataSourceItemHandler
{
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        var request = new VeevaRequest("objects/objectworkflows/tasks").AddQueryParameter("assignee__v", "me()");
        var entities = await Client.PaginateOffset<WorkflowTaskEntity>(request, maxPages: 2);

        if (!string.IsNullOrEmpty(context.SearchString))
            entities = entities.Where(x => x.Label.MatchesSearch(context.SearchString)).ToList();

        return entities.Select(x => new DataSourceItem(x.Id.ToString(), x.Label)).ToList();
    }
}