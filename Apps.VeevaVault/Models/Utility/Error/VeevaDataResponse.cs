using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Utility.Error;

public class VeevaDataResponse<T> : VeevaResponse
{
    [JsonProperty("data")]
    public T? Data { get; set; }
}