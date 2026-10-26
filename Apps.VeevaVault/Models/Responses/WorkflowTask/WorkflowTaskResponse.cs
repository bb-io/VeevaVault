using Apps.VeevaVault.Models.Entities.Workflow;
using Blackbird.Applications.Sdk.Common;

namespace Apps.VeevaVault.Models.Responses.WorkflowTask;

public class WorkflowTaskResponse(WorkflowTaskEntity entity)
{
    [Display("Workflow task ID")]
    public string Id { get; set; } = entity.Id.ToString();

    [Display("Workflow task label")]
    public string Label { get; set; } = entity.Label;

    [Display("Workflow task instructions")]
    public string? Instructions { get; set; } = entity.Instructions;

    [Display("Workflow task status")]
    public string Status { get; set; } = entity.Status.First();

    [Display("Workflow task created date")]
    public DateTime CreatedDate { get; set; } = entity.CreatedDate;

    [Display("Workflow task assigned date")]
    public DateTime? AssignedDate { get; set; } = entity.AssignedDate;

    [Display("Due date")]
    public DateTime? DueDate { get; set; } = entity.DueDate;

    [Display("Assignee ID")]
    public string? AssigneeId { get; set; } = entity.AssigneeId;
}