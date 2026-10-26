using Apps.VeevaVault.Constants;
using Apps.VeevaVault.Events.Polling.Models.Requests.Document;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Helper;
using Apps.VeevaVault.Models.Entities.Document;
using Apps.VeevaVault.Models.Polling;
using Apps.VeevaVault.Models.Responses.Document;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;

namespace Apps.VeevaVault.Events.Polling;

[PollingEventList("Documents")]
public class DocumentPollingList(InvocationContext invocationContext) : VeevaVaultInvocable(invocationContext)
{
    [MultipleEvents]
    [PollingEvent("On document created or updated", Description = "Triggered when a document is created or updated.")]
    public async Task<PollingEventResponse<DateMemory, List<DocumentResponse>>> OnDocumentCreatedOrUpdated(
        PollingEventRequest<DateMemory> pollingRequest,
        [PollingEventParameter] OnDocumentCreatedOrUpdatedRequest input)
    {
        var pollingStartTime = DateTime.UtcNow;

        if (pollingRequest.Memory?.LastPollingTime is null)
            return PollingResponses.DoNotFly<List<DocumentResponse>>(pollingStartTime);
        
        var documentIds = await DocumentFilterHelper.GetDocumentIds(Client, input.BinderId, input.WorkflowStep);
        if (documentIds is { Count: 0 })
            return PollingResponses.DoNotFly<List<DocumentResponse>>(pollingStartTime);
        
        string vql = new VqlBuilder("documents", VqlFields.Document)
            .Find(input.NameContains, "name__v")
            .Where("binder__v = false")
            .WhereContains("TONAME(type__v)", input.DocumentTypes)
            .WhereContains("id", documentIds)
            .WhereAfterOrEqual("version_modified_date__v", pollingRequest.Memory?.LastPollingTime)
            .Build();
        
        var entities = await Client.Query<DocumentEntity>(vql);
        var response = entities.Select(x => new DocumentResponse(x)).ToList();

        return PollingResponses.FlyIfAny(pollingStartTime, response);
    }
}