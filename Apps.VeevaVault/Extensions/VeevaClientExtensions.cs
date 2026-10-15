using Apps.VeevaVault.Api;
using Apps.VeevaVault.Models.Entities.VaultObject;
using Apps.VeevaVault.Models.Utility.Wrapper;

namespace Apps.VeevaVault.Extensions;

public static class VeevaClientExtensions
{
    public static async Task<VaultObjectRecordEntity> GetObjectRecord(
        this VeevaClient client,
        string objectName,
        string recordId)
    {
        var request = new VeevaRequest($"vobjects/{objectName}/{recordId}");
        var response = await client.ExecuteWithErrorHandling<DataWrapper<VaultObjectRecordEntity>>(request);
        return response.Data;
    }
}