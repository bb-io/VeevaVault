using Apps.VeevaVault.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.VeevaVault.Connections;

public class ConnectionValidator(InvocationContext invocationContext) : BaseInvocable(invocationContext), IConnectionValidator
{
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        CancellationToken cancellationToken)
    {
        try
        {
            var client = new VeevaClient(authenticationCredentialsProviders);
            var request = new VeevaRequest();   // Lists api versions, requires auth

            // 403 check isn't enough because:
            // - the api will always return 200 OK
            // - we need to check if we can even ping the right instance because vaultDns (instance hostname) is a user input
            await client.ExecuteWithErrorHandling(request);
            return new ConnectionValidationResponse
            {
                IsValid = true,
                Message = "Success"
            };

        } 
        catch (PluginApplicationException ex)
        {
            InvocationContext.Logger?.LogError($"Connection validation failed: {ex.Message}", []);

            return new()
            {
                IsValid = false,
                Message = ex.Message
            };
        }

    }
}