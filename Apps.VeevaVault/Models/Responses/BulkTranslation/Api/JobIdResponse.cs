using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Responses.BulkTranslation.Api;

public class JobIdResponse
{
    [JsonProperty("jobId")]
    public string JobId { get; set; } = string.Empty;
}