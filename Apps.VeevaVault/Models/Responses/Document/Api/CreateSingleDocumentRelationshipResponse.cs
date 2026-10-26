using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Responses.Document.Api;

public class CreateSingleDocumentRelationshipResponse
{
    [JsonProperty("id")]
    public long Id { get; set; }
}