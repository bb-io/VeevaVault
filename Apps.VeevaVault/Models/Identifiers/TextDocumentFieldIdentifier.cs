using Apps.VeevaVault.Handlers.DocumentField;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.VeevaVault.Models.Identifiers;

public class TextDocumentFieldIdentifier
{
    [Display("Text document field name"), DataSource(typeof(TextDocumentFieldDataHandler))]
    public string FieldName { get; set; } = string.Empty;
}