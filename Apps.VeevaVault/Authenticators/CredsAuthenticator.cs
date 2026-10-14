using Apps.VeevaVault.Api;
using Apps.VeevaVault.Constants;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Models.Utility.Auth;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using RestSharp;
using RestSharp.Authenticators;

namespace Apps.VeevaVault.Authenticators;

public class CredsAuthenticator(IEnumerable<AuthenticationCredentialsProvider> creds) : IAuthenticator
{
    private string? _sessionId;
    
    public async ValueTask Authenticate(IRestClient client, RestRequest request)
    {
        if (string.IsNullOrEmpty(_sessionId))
        {
            string username = creds.Get(CredsNames.Username).Value;
            string password = creds.Get(CredsNames.Password).Value;

            using var veevaClient = new RestClient(VeevaClient.GetBaseUrl(creds));
            var sessionRequest = new VeevaRequest("auth", Method.Post) { AlwaysMultipartFormData = true }
                .AddParameter("username", username)
                .AddParameter("password", password);

            var sessionResponse = await veevaClient.ExecuteAsync(sessionRequest);
            var deserializedResponse = sessionResponse.DeserializeWithErrorHandling<CreateSessionIdResponse>();
            _sessionId = deserializedResponse.SessionId;
        }

        request.AddOrUpdateHeader("Authorization", _sessionId);
    }
}