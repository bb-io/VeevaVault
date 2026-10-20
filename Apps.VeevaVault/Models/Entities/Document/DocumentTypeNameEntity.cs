using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.Document;

public class DocumentTypeNameEntity
{
    [JsonProperty("type__v")]
    public string Type { get; set; } = string.Empty;
}