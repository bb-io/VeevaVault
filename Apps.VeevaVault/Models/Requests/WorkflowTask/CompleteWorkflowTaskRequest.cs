using Apps.VeevaVault.Handlers.VerdictPrompt;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.VeevaVault.Models.Requests.WorkflowTask;

public class CompleteWorkflowTaskRequest
{
    [Display("Reason", Description = "Required only if the selected verdict requires a reason")]
    [DataSource(typeof(ReasonDataHandler))]
    public string? Reason { get; set; }

    [Display("Capacity", Description = "Required only if the selected verdict requires a capacity")]
    [DataSource(typeof(CapacityDataHandler))]
    public string? Capacity { get; set; }

    [Display("Comment", Description = "Applied to multi-item (document) tasks only")]
    public string? Comment { get; set; }
}