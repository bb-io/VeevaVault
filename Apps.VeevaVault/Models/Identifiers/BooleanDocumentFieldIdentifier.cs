using Apps.VeevaVault.Handlers.DocumentField;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.VeevaVault.Models.Identifiers;

public class BooleanDocumentFieldIdentifier
{
    [Display("Boolean document field name"), DataSource(typeof(BooleanDocumentFieldDataHandler))]
    public string FieldName { get; set; } = string.Empty;
}