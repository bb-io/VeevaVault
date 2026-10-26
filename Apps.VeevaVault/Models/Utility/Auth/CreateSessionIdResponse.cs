using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Utility.Auth;

public class CreateSessionIdResponse
{
    [JsonProperty("sessionId")]
    public string SessionId { get; set; } = string.Empty;
}