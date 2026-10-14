using Apps.VeevaVault.Authenticators;
using Apps.VeevaVault.Constants;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Models.Utility.Pagination;
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
    public async Task<List<T>> Paginate<T>(VeevaRequest request, int? maxPages = null)
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