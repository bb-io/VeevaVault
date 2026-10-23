using Apps.VeevaVault.Api;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Models.Entities.Workflow;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Utility.Wrapper;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.VeevaVault.Handlers;

public class VerdictDataHandler : VeevaVaultInvocable, IAsyncDataSourceItemHandler
{
    private readonly string _taskId;
    
    public VerdictDataHandler(
        InvocationContext invocationContext, 
        [ActionParameter] WorkflowTaskIdentifier workflowTaskIdentifier) : base(invocationContext)
    {
        if (string.IsNullOrEmpty(workflowTaskIdentifier.TaskId))
            throw new PluginMisconfigurationException("Please specify a task ID first");

        _taskId = workflowTaskIdentifier.TaskId;
    }

    // https://general.veevavault.dev/vault-api/api-reference/26.2/workflows/workflow-tasks/retrieve-workflow-task-action-details
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        string action = await Client.GetCompleteActionName(_taskId);

        var request = new VeevaRequest($"objects/objectworkflows/tasks/{_taskId}/actions/{action}");
        var response = await Client.ExecuteWithErrorHandling<DataWrapper<WorkflowTaskActionEntity>>(request);

        return response.Data.Controls
            .SelectMany(x => x.Verdicts)
            .Where(x => x.Label.MatchesSearch(context.SearchString))
            .Select(x => new DataSourceItem(x.Name, x.Label))
            .ToList();
    }
}