using Apps.VeevaVault.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.VeevaVault.Models.Requests.Document;

public class DownloadDocumentRequest
{
    [Display("Document version"), DataSource(typeof(DocumentVersionDataHandler))]
    public string Version { get; set; } = string.Empty;
}