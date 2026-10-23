using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.Workflow;

public class ActionControl
{
    [JsonProperty("verdicts", NullValueHandling = NullValueHandling.Ignore)]
    public List<ActionVerdict> Verdicts { get; set; } = [];
}