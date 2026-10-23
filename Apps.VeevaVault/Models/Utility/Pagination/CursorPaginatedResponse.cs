using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Utility.Pagination;

public class CursorPaginatedResponse<T>
{
    [JsonProperty("responseDetails")]
    public CursorPaginationDetails? Pagination { get; set; }

    [JsonProperty("data")]
    public List<T> Data { get; set; } = [];
}