using Apps.VeevaVault.Handlers.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.VeevaVault.Models.Requests.BulkTranslation;

public class ExportBulkTranslationFileRequest
{
    [Display("Message type"), StaticDataSource(typeof(MessageTypeStaticDataHandler))]
    public string MessageType { get; set; } = string.Empty;
}