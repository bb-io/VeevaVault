using Apps.VeevaVault.Models.Entities.Document;
using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Responses.Document.Api;

public class RetrieveAllDocumentTypesResponse
{
    [JsonProperty("types")]
    public List<DocumentTypeEntity> Types { get; set; } = [];
}