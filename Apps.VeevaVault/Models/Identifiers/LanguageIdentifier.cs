using Apps.VeevaVault.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.VeevaVault.Models.Identifiers;

public class LanguageIdentifier
{
    [Display("Language key"), DataSource(typeof(LanguageDataHandler))]
    public string LanguageKey { get; set; } = string.Empty;
}