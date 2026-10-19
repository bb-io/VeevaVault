using Apps.VeevaVault.Models.Entities.Document;
using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Responses.Document.Api;

public class RetrieveAllDocumentFieldsResponse
{
    [JsonProperty("properties")]
    public List<DocumentFieldEntity> Properties { get; set; } = [];
}