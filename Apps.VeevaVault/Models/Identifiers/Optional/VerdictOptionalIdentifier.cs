using Apps.VeevaVault.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.VeevaVault.Models.Identifiers.Optional;

public class VerdictOptionalIdentifier
{
    [Display("Verdict"), DataSource(typeof(VerdictDataHandler))]
    public string? Verdict { get; set; }
}