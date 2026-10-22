using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.Language;

public class LanguageEntity
{
    [JsonProperty("name__v")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("admin_key__sys")]
    public string AdminKey { get; set; } = string.Empty;
}