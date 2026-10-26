using Apps.VeevaVault.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;

namespace Apps.VeevaVault.Models.Requests.Document;

public class UpdateDocumentRequest
{
    [Display("Document field names"), DataSource(typeof(DocumentFieldDataHandler))]
    public List<string> DocumentFieldNames { get; set; } = [];

    [Display("Document field values", Description = "Corresponds to the 'Document field names' input")]
    public List<string> DocumentFieldValues { get; set; } = [];

    public void Validate()
    {
        if (DocumentFieldNames.Count != DocumentFieldValues.Count)
        {
            throw new PluginMisconfigurationException(
                "The 'Document field names' must correspond to the 'Document field values' input. " +
                "Their length must be the same");
        }
    }
}