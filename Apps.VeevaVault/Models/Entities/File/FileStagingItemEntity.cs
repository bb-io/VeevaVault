using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.File;

public class FileStagingItemEntity
{
    [JsonProperty("path")]
    public string Path { get; set; } = string.Empty;
}