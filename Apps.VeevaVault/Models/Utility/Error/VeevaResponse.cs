using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Utility.Error;

public class VeevaResponse
{
    [JsonProperty("responseStatus")]
    public string ResponseStatus { get; set; } = string.Empty;

    [JsonProperty("errors")]
    public List<VeevaError>? Errors { get; set; }

    public bool IsError => string.Equals(ResponseStatus, "FAILURE", StringComparison.OrdinalIgnoreCase);

    public string GetErrorMessage()
    {
        var errors = Errors?.Select(e => $"{e.Type}: {e.Message}").ToList() ?? [];
        return errors.Count > 0 ? string.Join("; ", errors) : "Vault returned FAILURE without error details.";
    }
}