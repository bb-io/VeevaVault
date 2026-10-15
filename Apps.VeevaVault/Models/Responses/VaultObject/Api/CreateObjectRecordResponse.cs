using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Responses.VaultObject.Api;

public class CreateObjectRecordResponse
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;
}