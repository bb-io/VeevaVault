using Apps.VeevaVault.Models.Entities.VaultObject;
using Blackbird.Applications.Sdk.Common;

namespace Apps.VeevaVault.Models.Responses.VaultObject;

public class VaultObjectRecordResponse(VaultObjectRecordEntity entity)
{
    [Display("Vault object ID")]
    public string Id { get; set; } = entity.Id;

    [Display("Vault object name")]
    public string Name { get; set; } = entity.Name;
}