using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.VaultObject;

public class VaultObjectRecordEntity
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("name__v")]
    public string Name { get; set; } = string.Empty;
}