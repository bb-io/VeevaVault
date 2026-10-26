using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.VeevaVault.Models.Requests.Document;

public class UploadDocumentAttachmentRequest
{
    [Display("File")]
    public FileReference File { get; set; } = null!;
}