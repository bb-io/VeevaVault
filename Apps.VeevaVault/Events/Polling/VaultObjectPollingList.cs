using Apps.VeevaVault.Api;
using Apps.VeevaVault.Constants;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Helper;
using Apps.VeevaVault.Models.Entities.VaultObject;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Polling;
using Apps.VeevaVault.Models.Responses.VaultObject;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;
using RestSharp;

namespace Apps.VeevaVault.Events.Polling;

[PollingEventList("Vault objects")]
public class VaultObjectPollingList(InvocationContext invocationContext) : VeevaVaultInvocable(invocationContext)
{
    [MultipleEvents]
    [PollingEvent("On object record created or updated",
        Description = "Triggered when a vault object record is created or updated.")]
    public async Task<PollingEventResponse<DateMemory, List<VaultObjectRecordMinimalResponse>>> OnObjectRecordCreatedOrUpdated(
        PollingEventRequest<DateMemory> pollingRequest,
        [PollingEventParameter] VaultObjectIdentifier objectIdentifier)
    {
        var pollingStartTime = DateTime.UtcNow;

        if (pollingRequest.Memory?.LastPollingTime is null)
            return PollingResponses.DoNotFly<List<VaultObjectRecordMinimalResponse>>(pollingStartTime);

        string mainSelect = VqlQueries.GetMinimalVaultObjectQuery(objectIdentifier.VaultObjectName);
        string timestamp = pollingRequest.Memory.LastPollingTime.Value.ToVeevaTimestampString();
        string vql = $"{mainSelect} WHERE modified_date__v >= '{timestamp}'";
        
        var request = new VeevaRequest("query", Method.Post).AddVql(vql);
        var response = await Client.Paginate<VaultObjectRecordMinimalEntity>(request);
        var items = response.Select(x => new VaultObjectRecordMinimalResponse(x)).ToList();
        
        return PollingResponses.FlyIfAny(pollingStartTime, items);
    }
}