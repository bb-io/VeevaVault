using Apps.VeevaVault.Models.Responses.BulkTranslation.Api;
using Blackbird.Applications.Sdk.Common;

namespace Apps.VeevaVault.Models.Responses.BulkTranslation;

public class ImportBulkTranslationsFileResponse(RetrieveImportJobSummaryResponse response)
{
    [Display("Ignored")]
    public long Ignored { get; set; } = response.Ignored;

    [Display("Updated")]
    public long Updated { get; set; } = response.Updated;

    [Display("Failed")]
    public long Failed { get; set; } = response.Failed;

    [Display("Added")]
    public long Added { get; set; } = response.Added;
}