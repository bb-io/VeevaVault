using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.Document;

public class DocumentSubtypeEntity
{
    [JsonProperty("label")]
    public string Label { get; set; } = string.Empty;

    [JsonProperty("value")]
    public string Value { get; set; } = string.Empty;
}