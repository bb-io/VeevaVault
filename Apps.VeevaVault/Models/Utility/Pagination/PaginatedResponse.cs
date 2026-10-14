using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Utility.Pagination;

public class PaginatedResponse<T>
{
    [JsonProperty("responseDetails")]
    public PaginationDetails? Pagination { get; set; }

    [JsonProperty("data")]
    public List<T> Data { get; set; } = [];
}