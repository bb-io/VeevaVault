using Apps.VeevaVault.Constants;
using RestSharp;

namespace Apps.VeevaVault.Api;

public class VeevaRequest(string endpoint = "", Method method = Method.Get, string apiVersion = ApiVersion.V26P3) 
    : RestRequest($"{apiVersion}/{endpoint.TrimStart('/')}", method);