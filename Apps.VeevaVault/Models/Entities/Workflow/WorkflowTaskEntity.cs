using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.Workflow;

public class WorkflowTaskEntity
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("label__v")]
    public string Label { get; set; } = string.Empty;

    [JsonProperty("instructions__v")]
    public string? Instructions { get; set; }

    [JsonProperty("status__v")]
    public string[] Status { get; set; } = [];

    [JsonProperty("created_date__v")]
    public DateTime CreatedDate { get; set; }

    [JsonProperty("assigned_date__v")]
    public DateTime? AssignedDate { get; set; }

    [JsonProperty("due_date__v")]
    public DateTime? DueDate { get; set; }

    [JsonProperty("assignee__v")]
    public string? AssigneeId { get; set; }
    
    [JsonProperty("object__v")]
    public string? ObjectName { get; set; }

    [JsonIgnore]
    public bool IsMultiItem => ObjectName == "envelope__sys";
}