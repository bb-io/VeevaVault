using Apps.VeevaVault.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.VeevaVault.Models.Identifiers;

public class DocumentUserActionIdentifier
{
    [Display("Action name"), DataSource(typeof(DocumentUserActionDataHandler))]
    public string ActionName { get; set; } = string.Empty;
}