using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.Workflow;

public class WorkflowTaskItemEntity
{
    [JsonProperty("document_id__sys")]
    public long DocumentId { get; set; }
}