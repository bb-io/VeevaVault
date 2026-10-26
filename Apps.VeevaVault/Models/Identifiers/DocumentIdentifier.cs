using Apps.VeevaVault.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.VeevaVault.Models.Identifiers;

public class DocumentIdentifier
{
    [Display("Document ID"), DataSource(typeof(DocumentDataHandler))]
    public string DocumentId { get; set; } = string.Empty;
}