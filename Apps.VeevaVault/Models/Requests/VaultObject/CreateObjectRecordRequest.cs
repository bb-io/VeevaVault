using Blackbird.Applications.Sdk.Common;

namespace Apps.VeevaVault.Models.Requests.VaultObject;

public class CreateObjectRecordRequest
{
    [Display("Vault object name")]
    public string Name { get; set; } = string.Empty;
}