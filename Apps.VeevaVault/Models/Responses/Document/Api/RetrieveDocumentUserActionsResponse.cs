using Apps.VeevaVault.Models.Entities.Document;
using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Responses.Document.Api;

public class RetrieveDocumentUserActionsResponse
{
    [JsonProperty("lifecycle_actions__v")]
    public List<DocumentUserActionEntity> DocumentUserActions { get; set; } = [];
}