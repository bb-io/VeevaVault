using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.VaultObject;

public class VaultObjectField
{
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("label")]
    public string Label { get; set; } = string.Empty;

    [JsonProperty("editable")]
    public bool IsEditable { get; set; }
}