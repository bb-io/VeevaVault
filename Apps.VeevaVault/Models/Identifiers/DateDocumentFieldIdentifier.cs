using Apps.VeevaVault.Handlers.DocumentField;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.VeevaVault.Models.Identifiers;

public class DateDocumentFieldIdentifier
{
    [Display("Date document field name"), DataSource(typeof(DateDocumentFieldDataHandler))]
    public string FieldName { get; set; } = string.Empty;
}