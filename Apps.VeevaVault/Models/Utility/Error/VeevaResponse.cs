using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Utility.Error;

public class VeevaResponse
{
    [JsonProperty("responseStatus")]
    public string ResponseStatus { get; set; } = string.Empty;

    [JsonProperty("errors")]
    public List<VeevaError>? Errors { get; set; }

    public bool IsError => string.Equals(ResponseStatus, "FAILURE", StringComparison.OrdinalIgnoreCase);
}