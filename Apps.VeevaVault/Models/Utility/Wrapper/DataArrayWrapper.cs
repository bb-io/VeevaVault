using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Utility.Wrapper;

public class DataArrayWrapper<T>
{
    [JsonProperty("data")]
    public List<T> Data { get; set; } = [];
}