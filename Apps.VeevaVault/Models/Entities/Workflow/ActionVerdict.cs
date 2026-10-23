using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.Workflow;

public class ActionVerdict
{
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("label")]
    public string Label { get; set; } = string.Empty;

    [JsonProperty("reasons")]
    public ActionVerdictPromptGroup? Reasons { get; set; }

    [JsonProperty("capacities")]
    public ActionVerdictPromptGroup? Capacities { get; set; }
}