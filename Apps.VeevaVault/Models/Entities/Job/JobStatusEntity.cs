using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.Job;

public class JobStatusEntity
{
    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;

    [JsonProperty("links")]
    public List<JobStatusLink> Links { get; set; } = [];
}