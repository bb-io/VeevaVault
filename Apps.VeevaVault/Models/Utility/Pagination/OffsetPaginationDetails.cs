using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Utility.Pagination;

public class OffsetPaginationDetails
{
    [JsonProperty("total")]
    public int Total { get; set; }

    [JsonProperty("offset")]
    public int Offset { get; set; }

    [JsonProperty("page_size")]
    public int PageSize { get; set; }
}