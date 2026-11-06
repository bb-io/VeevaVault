using Apps.VeevaVault.Handlers.DocumentField;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.VeevaVault.Models.Identifiers;

public class NumberDocumentFieldIdentifier
{
    [Display("Number document field name"), DataSource(typeof(NumberDocumentFieldDataHandler))]
    public string FieldName { get; set; } = string.Empty;
}