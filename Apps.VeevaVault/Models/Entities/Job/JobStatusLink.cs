using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.Job;

public class JobStatusLink
{
    [JsonProperty("rel")]
    public string Rel { get; set; } = string.Empty;

    [JsonProperty("href")]
    public string Href { get; set; } = string.Empty;
}