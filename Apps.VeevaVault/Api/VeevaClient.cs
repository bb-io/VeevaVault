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
    
    public async Task<List<T>> PaginateCursor<T>(RestRequest request, int? maxPages = null)
    {
        var response = await ExecuteWithErrorHandling<CursorPaginatedResponse<T>>(request);
        var results = response.Data;
        string? nextUrl = response.Pagination?.NextPage;
        int pagesFetched = 1;

        while (!string.IsNullOrWhiteSpace(nextUrl) && (maxPages is null || pagesFetched < maxPages))
        {
            var nextUri = new Uri(Options.BaseUrl!, "/" + nextUrl.TrimStart('/'));
            response = await ExecuteWithErrorHandling<CursorPaginatedResponse<T>>(new RestRequest(nextUri, Method.Post));

            results.AddRange(response.Data);
            nextUrl = response.Pagination?.NextPage;
            pagesFetched++;
        }

        return results;
    }

    public async Task<List<T>> PaginateOffset<T>(RestRequest request, int? maxPages = null)
    {
        var results = new List<T>();
        int offset = 0;
        int pagesFetched = 0;

        while (maxPages is null || pagesFetched < maxPages)
        {
            request.AddOrUpdateParameter("page_size", 100);
            request.AddOrUpdateParameter("offset", offset);

            var response = await ExecuteWithErrorHandling<OffsetPaginatedResponse<T>>(request);
            results.AddRange(response.Data);
            pagesFetched++;

            int total = response.Pagination?.Total ?? 0;
            if (response.Data.Count == 0 || results.Count >= total)
                break;

            offset += response.Data.Count;
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
        string input = creds.Get(CredsNames.VaultDns).Value.Trim();
        string host = Uri.TryCreate(input, UriKind.Absolute, out var parsed) && !string.IsNullOrEmpty(parsed.Host)
            ? parsed.Host
            : input.Split('/')[0];

        if (Uri.CheckHostName(host) == UriHostNameType.Unknown)
        {
            throw new PluginMisconfigurationException(
                $"'{input}' is not a valid Vault host name. Valid format: 'yourcompany.veevavault.com'");
        }
        
        return new Uri($"https://{host}/api/");
    }
    
    public RestRequest CreateRequestFromHref(string href, Method method = Method.Get)
    {
        var uri = new Uri(Options.BaseUrl!, "/" + href.TrimStart('/'));
        return new RestRequest(uri, method);
    }
    
    protected override Exception ConfigureErrorException(RestResponse response)
    {
        string error = response.ExtractErrors() ?? $"Request failed with status code {response.StatusCode}.";
        return new PluginApplicationException(error);
    }
}