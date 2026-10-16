using Apps.VeevaVault.Models.Utility.Error;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.VeevaVault.Extensions;

public static class RestResponseExtensions
{
    public static T DeserializeWithErrorHandling<T>(this RestResponse response)
    {
        string? error = response.ExtractErrors();
        if (error is not null)
            throw new PluginApplicationException(error);

        if (string.IsNullOrWhiteSpace(response.Content))
            throw new PluginApplicationException("Vault returned an empty response.");

        return JsonConvert.DeserializeObject<T>(response.Content)
               ?? throw new PluginApplicationException($"Could not deserialize the response. Raw: {response.Content}");
    }

    public static string? ExtractErrors(this RestResponse response)
    {
        var veevaResponse = TryParseEnvelope(response.Content);
        if (veevaResponse is null)
        {
            return response.IsSuccessful 
                ? null 
                : response.ErrorMessage ?? $"Request failed with status {(int)response.StatusCode}. {response.Content}".TrimEnd();
        }
        
        return !veevaResponse.IsError ? null : veevaResponse.GetErrorMessage();
    }

    private static VeevaResponse? TryParseEnvelope(string? content)
    {
        if (string.IsNullOrWhiteSpace(content))
            return null;

        try
        {
            return JsonConvert.DeserializeObject<VeevaResponse>(content);
        }
        catch (JsonException)
        {
            return null;
        }
    }
}