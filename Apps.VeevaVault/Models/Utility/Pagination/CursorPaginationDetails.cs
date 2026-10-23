using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Utility.Pagination;

public class CursorPaginationDetails
{
    [JsonProperty("next_page")]
    public string? NextPage { get; set; }
}