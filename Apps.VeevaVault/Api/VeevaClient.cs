using Apps.VeevaVault.Authenticators;
using Apps.VeevaVault.Constants;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Models.Utility.Error;
using Apps.VeevaVault.Models.Utility.Pagination;
using Apps.VeevaVault.Models.Utility.Wrapper;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using RestSharp;

namespace Apps.VeevaVault.Api;

public class VeevaClient(IEnumerable<AuthenticationCredentialsProvider> creds) : BlackBirdRestClient(new()
{
    BaseUrl = GetBaseUrl(creds),
    Authenticator = new CredsAuthenticator(creds),
})
{
    public async Task<List<T>> ExecuteBulkWithErrorHandling<T>(RestRequest request)
    {
        var response = await ExecuteWithErrorHandling<DataWrapper<List<VeevaDataResponse<T>>>>(request);

        var errors = response.Data
            .Select((item, index) => item.IsError ? $"Record {index + 1}: {item.GetErrorMessage()}" : null)
            .OfType<string>()
            .ToList();

        if (errors.Count > 0)
            throw new PluginApplicationException(string.Join(" | ", errors));

        return response.Data.Select(x => x.Data).OfType<T>().ToList();
    }
    
    public async Task<List<T>> Paginate<T>(RestRequest request, int? maxPages = null)
    {
        var response = await ExecuteWithErrorHandling<PaginatedResponse<T>>(request);
        var results = response.Data;
        string? nextUrl = response.Pagination?.NextPage;
        int pagesFetched = 1;

        while (!string.IsNullOrWhiteSpace(nextUrl) && (maxPages is null || pagesFetched < maxPages))
        {
            var nextUri = new Uri(Options.BaseUrl!, "/" + nextUrl.TrimStart('/'));
            response = await ExecuteWithErrorHandling<PaginatedResponse<T>>(new RestRequest(nextUri, Method.Post));

            results.AddRange(response.Data);
            nextUrl = response.Pagination?.NextPage;
            pagesFetched++;
        }

        return results;
    }
    
    public override async Task<T> ExecuteWithErrorHandling<T>(RestRequest request)
    {
        var response = await ExecuteAsync(request);
        return response.DeserializeWithErrorHandling<T>();
    }

    public override async Task<RestResponse> ExecuteWithErrorHandling(RestRequest request)
    {
        var response = await ExecuteAsync(request);
        string? error = response.ExtractErrors();
        return error is null ? response : throw new PluginApplicationException(error);
    }
    
    public static Uri GetBaseUrl(IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        return new Uri($"https://{creds.Get(CredsNames.VaultDns).Value.TrimEnd('/')}/api/");
    }
    
    protected override Exception ConfigureErrorException(RestResponse response)
    {
        string error = response.ExtractErrors() ?? $"Request failed with status code {response.StatusCode}.";
        return new PluginApplicationException(error);
    }
}