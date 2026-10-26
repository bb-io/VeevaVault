using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.Document;

public class DocumentAttachmentEntity
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("filename__v")]
    public string Filename { get; set; } = string.Empty;
}