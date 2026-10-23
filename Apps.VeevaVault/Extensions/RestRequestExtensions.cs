using RestSharp;

namespace Apps.VeevaVault.Extensions;

public static class RestRequestExtensions
{
    public static RestRequest AddParameterIfNotEmpty(this RestRequest request, string paramName, string? paramValue)
    {
        if (string.IsNullOrWhiteSpace(paramValue))
            return request;

        request.AddParameter(paramName, paramValue);
        return request;
    }
}