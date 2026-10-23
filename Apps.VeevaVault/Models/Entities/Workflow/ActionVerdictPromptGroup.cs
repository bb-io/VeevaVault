using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.Workflow;

public class ActionVerdictPromptGroup
{
    [JsonProperty("label")]
    public string Label { get; set; } = string.Empty;

    [JsonProperty("required")]
    public bool Required { get; set; }

    [JsonProperty("prompts")]
    public List<ActionVerdictPrompt> Prompts { get; set; } = [];
}