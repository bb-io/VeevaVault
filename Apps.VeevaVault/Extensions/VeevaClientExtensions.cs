using Apps.VeevaVault.Api;
using Apps.VeevaVault.Models.Entities.VaultObject;
using Apps.VeevaVault.Models.Utility.Wrapper;
using RestSharp;

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

    public static Task<List<T>> Query<T>(this VeevaClient client, string vql, int? maxPages = null)
    {
        var request = new VeevaRequest("query", Method.Post).AddQueryParameter("q", vql);
        return client.Paginate<T>(request, maxPages);
    }
}