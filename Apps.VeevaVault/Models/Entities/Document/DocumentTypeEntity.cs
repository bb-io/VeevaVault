using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.Document;

public class DocumentTypeEntity
{
    [JsonProperty("value")]
    public string Value { get; set; } = string.Empty;
    
    [JsonProperty("label")]
    public string Label { get; set; } = string.Empty;
}