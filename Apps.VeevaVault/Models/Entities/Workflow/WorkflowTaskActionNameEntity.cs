using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.Workflow;

public class WorkflowTaskActionNameEntity
{
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("label")]
    public string Label { get; set; } = string.Empty;
}