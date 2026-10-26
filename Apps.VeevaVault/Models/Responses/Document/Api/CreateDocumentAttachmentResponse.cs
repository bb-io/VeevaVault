using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Responses.Document.Api;

public class CreateDocumentAttachmentResponse
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("version__v")]
    public long Version { get; set; }
}