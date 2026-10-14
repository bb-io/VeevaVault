using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Utility.Error;

public class VeevaError
{
    [JsonProperty("type")]
    public string? Type { get; set; }
    
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;
}