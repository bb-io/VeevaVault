using Apps.VeevaVault.Handlers.DocumentField;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.VeevaVault.Models.Identifiers;

public class PicklistDocumentFieldIdentifier
{
    [Display("Picklist document field name"), DataSource(typeof(PicklistDocumentFieldDataHandler))]
    public string FieldName { get; set; } = string.Empty;
}