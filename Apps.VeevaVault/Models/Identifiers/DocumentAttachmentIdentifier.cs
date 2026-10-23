using Apps.VeevaVault.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.VeevaVault.Models.Identifiers;

public class DocumentAttachmentIdentifier
{
    [Display("Attachment ID"), DataSource(typeof(DocumentAttachmentDataHandler))]
    public string AttachmentId { get; set; } = string.Empty;
}