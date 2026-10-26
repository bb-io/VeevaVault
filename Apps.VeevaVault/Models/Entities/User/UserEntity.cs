using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.User;

public class UserEntity
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("name__v")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("email__sys")]
    public string Email { get; set; } = string.Empty;
}