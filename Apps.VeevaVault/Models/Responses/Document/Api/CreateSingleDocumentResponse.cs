using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Responses.Document.Api;

public class CreateSingleDocumentResponse
{
    [JsonProperty("id")]
    public long Id { get; set; }
}