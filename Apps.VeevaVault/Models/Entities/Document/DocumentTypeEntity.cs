using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.Document;

public class DocumentTypeEntity
{
    [JsonProperty("label")]
    public string Label { get; set; } = string.Empty;
}