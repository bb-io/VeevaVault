using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Utility.Wrapper;

public class DataWrapper<T>
{
    [JsonProperty("data")]
    public T Data { get; set; } = default!;
}