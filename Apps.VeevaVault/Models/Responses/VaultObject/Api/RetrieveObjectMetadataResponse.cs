using Apps.VeevaVault.Models.Entities.VaultObject;
using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Responses.VaultObject.Api;

public class RetrieveObjectMetadataResponse
{
    [JsonProperty("object")]
    public VaultObjectEntity Object { get; set; } = null!;
}