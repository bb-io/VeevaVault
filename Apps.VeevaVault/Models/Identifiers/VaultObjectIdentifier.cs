using Apps.VeevaVault.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.VeevaVault.Models.Identifiers;

public class VaultObjectIdentifier
{
    [Display("Value object name"), DataSource(typeof(VaultObjectDataHandler))]
    public string VaultObjectName { get; set; } = string.Empty;
}