using Apps.VeevaVault.Models.Entities.Workflow;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Identifiers.Optional;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.VeevaVault.Handlers.VerdictPrompt;

public class CapacityDataHandler(
    InvocationContext invocationContext,
    [ActionParameter] WorkflowTaskIdentifier taskIdentifier,
    [ActionParameter] VerdictOptionalIdentifier verdictIdentifier)
    : VerdictPromptDataHandler(invocationContext, taskIdentifier, verdictIdentifier)
{
    protected override ActionVerdictPromptGroup? SelectGroup(ActionVerdict verdict) => verdict.Capacities;
}