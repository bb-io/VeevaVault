using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.VeevaVault.Models.Requests.BulkTranslation;

public class ImportBulkTranslationsFileRequest
{
    [Display("File")]
    public FileReference File { get; set; } = null!;
}