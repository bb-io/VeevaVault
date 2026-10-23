using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.Common;

public class IdEntity
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;
}