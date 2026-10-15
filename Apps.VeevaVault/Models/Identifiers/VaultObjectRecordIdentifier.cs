using Apps.VeevaVault.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.VeevaVault.Models.Identifiers;

public class VaultObjectRecordIdentifier
{
    [Display("Vault object record ID"), DataSource(typeof(VaultObjectRecordDataHandler))]
    public string VaultObjectRecordId { get; set; } = string.Empty;
}