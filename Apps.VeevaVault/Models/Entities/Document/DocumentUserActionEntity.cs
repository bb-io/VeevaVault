using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.Document;

public class DocumentUserActionEntity
{
    [JsonProperty("name__v")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("label__v")]
    public string Label { get; set; } = string.Empty;
}