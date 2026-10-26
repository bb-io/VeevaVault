using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Models.Entities.Workflow;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Identifiers.Optional;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.VeevaVault.Handlers.VerdictPrompt;

public abstract class VerdictPromptDataHandler : VeevaVaultInvocable, IAsyncDataSourceItemHandler
{
    private readonly string _taskId;
    private readonly string _verdict;

    protected VerdictPromptDataHandler(
        InvocationContext invocationContext,
        WorkflowTaskIdentifier taskIdentifier,
        VerdictOptionalIdentifier verdictIdentifier) : base(invocationContext)
    {
        if (string.IsNullOrWhiteSpace(taskIdentifier.TaskId))
            throw new PluginMisconfigurationException("Please specify a task ID first");
        
        if (string.IsNullOrWhiteSpace(verdictIdentifier.Verdict))
            throw new PluginMisconfigurationException("Please select a verdict first");
        
        _taskId = taskIdentifier.TaskId;
        _verdict = verdictIdentifier.Verdict;
    }

    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        var verdicts = await Client.GetTaskVerdicts(_taskId);
        var verdict = verdicts.FirstOrDefault(x => x.Name == _verdict);

        return (verdict is null ? null : SelectGroup(verdict))?.Prompts
            .Where(x => x.Label.MatchesSearch(context.SearchString))
            .Select(x => new DataSourceItem(x.Name, x.Label))
            .ToList() ?? [];
    }

    protected abstract ActionVerdictPromptGroup? SelectGroup(ActionVerdict verdict);
}