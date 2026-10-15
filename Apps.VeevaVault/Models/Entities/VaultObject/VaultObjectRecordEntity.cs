using Newtonsoft.Json;

namespace Apps.VeevaVault.Models.Entities.VaultObject;

public class VaultObjectRecordEntity
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("name__v")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("created_date__v")]
    public DateTime CreatedDate { get; set; }

    [JsonProperty("created_by__v")]
    public int CreatedBy { get; set; }

    [JsonProperty("modified_date__v")]
    public DateTime ModifiedDate { get; set; }

    [JsonProperty("modified_by__v")]
    public int ModifiedBy { get; set; }
}