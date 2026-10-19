using Apps.VeevaVault.Models.Responses.Document.Api;
using Blackbird.Applications.Sdk.Common;

namespace Apps.VeevaVault.Models.Responses.Document;

public class UploadDocumentAttachmentResponse(CreateDocumentAttachmentResponse response)
{
    [Display("Attachment ID")]
    public string Id { get; set; } = response.Id;

    [Display("Attachment version")]
    public string Version { get; set; } = response.Version.ToString();
}