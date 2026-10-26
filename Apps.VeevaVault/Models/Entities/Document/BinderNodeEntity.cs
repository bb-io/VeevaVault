using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.Document;

public class BinderNodeEntity
{
    [JsonProperty("content_id__sys")] 
    public long ContentId { get; set; }
}