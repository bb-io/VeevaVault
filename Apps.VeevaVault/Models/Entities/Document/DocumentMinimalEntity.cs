using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.Document;

public class DocumentMinimalEntity
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name__v")]
    public string Name { get; set; } = string.Empty;
}