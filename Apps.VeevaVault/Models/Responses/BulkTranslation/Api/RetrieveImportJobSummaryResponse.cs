using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Responses.BulkTranslation.Api;

public class RetrieveImportJobSummaryResponse
{
    [JsonProperty("ignored")]
    public long Ignored { get; set; }

    [JsonProperty("updated")]
    public long Updated { get; set; }

    [JsonProperty("failed")]
    public long Failed { get; set; }

    [JsonProperty("added")]
    public long Added { get; set; }
}