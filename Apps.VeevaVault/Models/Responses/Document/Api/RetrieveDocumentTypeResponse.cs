using Apps.VeevaVault.Models.Entities.Document;
using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Responses.Document.Api;

public class RetrieveDocumentTypeResponse
{
    [JsonProperty("availableLifecycles")]
    public List<DocumentTypeLifecycleEntity> AvailableLifecycles { get; set; } = [];

    [JsonProperty("subtypes")]
    public List<DocumentSubtypeEntity> Subtypes { get; set; } = [];
}