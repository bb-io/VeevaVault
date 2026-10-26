using Apps.VeevaVault.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.VeevaVault.Models.Identifiers;

public class DocumentTypeIdentifier
{
    [Display("Document type"), DataSource(typeof(DocumentTypeDataHandler))]
    public string DocumentType { get; set; } = string.Empty;
}