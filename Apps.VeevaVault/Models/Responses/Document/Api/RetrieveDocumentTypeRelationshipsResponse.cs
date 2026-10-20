using Apps.VeevaVault.Models.Entities.Document;
using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Responses.Document.Api;

public class RetrieveDocumentTypeRelationshipsResponse
{
    [JsonProperty("relationshipTypes")]
    public List<DocumentRelationshipTypeEntity> RelationshipTypes { get; set; } = [];
}