using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.Document;

public class DocumentFieldEntity
{
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("label")]
    public string? Label { get; set; }

    [JsonProperty("editable")]
    public bool Editable { get; set; }

    [JsonProperty("disabled")]
    public bool Disabled { get; set; }

    [JsonProperty("hidden")]
    public bool Hidden { get; set; }
}