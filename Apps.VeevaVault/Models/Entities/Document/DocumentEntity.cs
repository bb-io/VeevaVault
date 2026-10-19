using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.Document;

public class DocumentEntity
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name__v")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("type__v")]
    public string Type { get; set; } = string.Empty;

    [JsonProperty("subtype__v")]
    public string Subtype { get; set; } = string.Empty;

    [JsonProperty("document_number__v")]
    public string DocumentNumber { get; set; } = string.Empty;

    [JsonProperty("description__v")]
    public string? Description { get; set; }

    [JsonProperty("status__v")]
    public string Status { get; set; } = string.Empty;

    [JsonProperty("document_creation_date__v")]
    public DateTime CreationDate { get; set; }
}