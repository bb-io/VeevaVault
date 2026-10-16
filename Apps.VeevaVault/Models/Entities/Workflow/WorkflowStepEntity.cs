using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.Workflow;

public class WorkflowStepEntity
{
    [JsonProperty("name__sys")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("label__sys")]
    public string Label { get; set; } = string.Empty;
}