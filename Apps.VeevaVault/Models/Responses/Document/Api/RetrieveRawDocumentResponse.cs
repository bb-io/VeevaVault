using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Apps.VeevaVault.Models.Responses.Document.Api;

public class RetrieveRawDocumentResponse
{
    [JsonProperty("document")]
    public JObject Document { get; set; } = new();
}