using Apps.VeevaVault.Models.Entities.VaultObject;
using Blackbird.Applications.Sdk.Common;

namespace Apps.VeevaVault.Models.Responses.VaultObject;

public class VaultObjectRecordResponse(VaultObjectRecordEntity entity)
{
    [Display("Vault object ID")]
    public string Id { get; set; } = entity.Id;

    [Display("Vault object name")]
    public string Name { get; set; } = entity.Name;

    [Display("Vault created date")]
    public DateTime CreatedDate { get; set; } = entity.CreatedDate;

    [Display("Vault created by")]
    public int CreatedBy { get; set; } = entity.CreatedBy;

    [Display("Vault modified date")]
    public DateTime ModifiedDate { get; set; } = entity.ModifiedDate;

    [Display("Vault modified by")]
    public int ModifiedBy { get; set; } = entity.ModifiedBy;
}