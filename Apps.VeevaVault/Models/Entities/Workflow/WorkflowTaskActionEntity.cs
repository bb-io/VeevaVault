using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.Workflow;

public class WorkflowTaskActionEntity
{
    [JsonProperty("controls")]
    public List<ActionControl> Controls { get; set; } = [];
}