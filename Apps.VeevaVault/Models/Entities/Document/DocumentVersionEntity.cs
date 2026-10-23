using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.Document;

public class DocumentVersionEntity
{
    [JsonProperty("number")]
    public string Number { get; set; } = string.Empty;
}