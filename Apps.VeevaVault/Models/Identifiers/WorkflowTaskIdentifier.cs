using Apps.VeevaVault.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.VeevaVault.Models.Identifiers;

public class WorkflowTaskIdentifier
{
    [Display("Workflow task ID"), DataSource(typeof(WorkflowTaskDataHandler))]
    public string TaskId { get; set; } = string.Empty;
}