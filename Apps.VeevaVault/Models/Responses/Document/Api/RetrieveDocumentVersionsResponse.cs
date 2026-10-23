using Apps.VeevaVault.Models.Entities.Document;
using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Responses.Document.Api;

public class RetrieveDocumentVersionsResponse
{
    [JsonProperty("versions")]
    public List<DocumentVersionEntity> Versions { get; set; } = [];
}