using Blackbird.Applications.Sdk.Common;

namespace Apps.VeevaVault.Models.Requests.VaultObject;

public class SearchObjectRecordsRequest
{
    [Display("Vault object name contains")]
    public string? NameContains { get; set; }
}