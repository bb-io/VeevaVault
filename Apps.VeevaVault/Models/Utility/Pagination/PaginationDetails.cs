using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Utility.Pagination;

public class PaginationDetails
{
    [JsonProperty("next_page")]
    public string? NextPage { get; set; }
}