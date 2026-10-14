using Apps.VeevaVault.Authenticators;
using Apps.VeevaVault.Constants;
using Apps.VeevaVault.Extensions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.VeevaVault.Api;

public class VeevaClient(IEnumerable<AuthenticationCredentialsProvider> creds) : BlackBirdRestClient(new()
{
    BaseUrl = GetBaseUrl(creds),
    Authenticator = new CredsAuthenticator(creds),
})
{
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