using Blackbird.Applications.Sdk.Common;

namespace Apps.VeevaVault.Models.Identifiers;

public class VaultObjectRecordIdentifier
{
    [Display("Vault object record ID")]
    public string VaultObjectRecordId { get; set; } = string.Empty;
}