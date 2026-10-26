using Apps.VeevaVault.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.VeevaVault.Models.Requests.Document;

public class CreateDocumentRequest
{
    [Display("File")]
    public FileReference File { get; set; } = null!;

    [Display("Document lifecycle"), DataSource(typeof(DocumentTypeLifecycleDataHandler))]
    public string Lifecycle { get; set; } = string.Empty;

    [Display("Document subtype", Description = "Required if the selected document type has subtypes")]
    [DataSource(typeof(DocumentSubtypeDataHandler))]
    public string? Subtype { get; set; }

    [Display("Custom document name", Description = "If empty, the file's name without an extension will be used instead")]
    public string? CustomName { get; set; }
}