using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Utility.Pagination;

public class OffsetPaginatedResponse<T>
{
    [JsonProperty("responseDetails")]
    public OffsetPaginationDetails? Pagination { get; set; }

    [JsonProperty("data")]
    public List<T> Data { get; set; } = [];
}