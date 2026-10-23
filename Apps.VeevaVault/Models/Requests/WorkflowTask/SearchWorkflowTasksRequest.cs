using Apps.VeevaVault.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.VeevaVault.Models.Requests.WorkflowTask;

public class SearchWorkflowTasksRequest
{
    [Display("Assignee ID", Description = "Defaults to the connected user")]
    [DataSource(typeof(UserDataHandler))]
    public string? AssigneeId { get; set; }
    
    [Display("Label contains")]
    public string? LabelContains { get; set; }
}