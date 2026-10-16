using Apps.VeevaVault.Models.Entities.Document;
using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Responses.Document.Api;

public class RetrieveDocumentResponse
{
    [JsonProperty("document")]
    public DocumentEntity Document { get; set; } = null!;
}