using Apps.VeevaVault.Models.Entities.VaultObject;
using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Responses.VaultObject.Api;

public class RetrieveVaultObjectsResponse
{
    [JsonProperty("objects")]
    public List<VaultObjectEntity> Objects { get; set; } = [];
}