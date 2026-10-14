using RestSharp;

namespace Apps.VeevaVault.Extensions;

public static class RestRequestExtensions
{
    // VQL = Vault Query Language language, very similar to SQL
    public static T AddVql<T>(this T request, string vql) 
        where T : RestRequest
    {
        if (string.IsNullOrWhiteSpace(vql))
            return request;

        request.AddQueryParameter("q", vql);
        return request;
    }
}