using Apps.VeevaVault.Api;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Models.Entities.VaultObject;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Requests.VaultObject;
using Apps.VeevaVault.Models.Responses.VaultObject;
using Apps.VeevaVault.Models.Responses.VaultObject.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.VeevaVault.Actions;

[ActionList("Vault objects")]
public class VaultObjectActions(InvocationContext invocationContext) : VeevaVaultInvocable(invocationContext)
{
    // https://general.veevavault.dev/vault-api/api-reference/26.2/vault-objects/retrieve-object-records/
    [Action("Search object records", Description = "Search object records with a specific criteria")]
    public async Task<SearchVaultObjectRecordsResponse> SearchObjectRecords(
        [ActionParameter] VaultObjectIdentifier objectIdentifier,
        [ActionParameter] SearchObjectRecordsRequest searchInput)
    {
        string vql = $"SELECT id, name__v FROM {objectIdentifier.VaultObjectName}";
        if (!string.IsNullOrEmpty(searchInput.NameContains))
            vql += $" FIND ('{searchInput.NameContains}' SCOPE name__v)";
        
        var request = new VeevaRequest("query", Method.Post).AddVql(vql);

        var response = await Client.Paginate<VaultObjectRecordMinimalEntity>(request);
        return new(response.Select(x => new VaultObjectRecordMinimalResponse(x)).ToList());
    }

    // https://general.veevavault.dev/vault-api/api-reference/26.2/vault-objects/retrieve-object-record/
    [Action("Get object record", Description = "Get details for a specific object record")]
    public async Task<VaultObjectRecordResponse> GetObjectRecord(
        [ActionParameter] VaultObjectIdentifier objectIdentifier,
        [ActionParameter] VaultObjectRecordIdentifier recordIdentifier)
    {
        var entity = await Client.GetObjectRecord(objectIdentifier.VaultObjectName, recordIdentifier.VaultObjectRecordId);
        return new(entity);
    }

    // https://general.veevavault.dev/vault-api/api-reference/26.2/vault-objects/create-upsert-object-records/
    [Action("Create object record", Description = "Create a new object record")]
    public async Task<VaultObjectRecordResponse> CreateObjectRecord(
        [ActionParameter] VaultObjectIdentifier objectIdentifier,
        [ActionParameter] CreateObjectRecordRequest input)
    {
        var body = new object[]
        {
            new
            {
                name__v = input.Name
            }
        };

        var request = new VeevaRequest($"vobjects/{objectIdentifier.VaultObjectName}", Method.Post).AddJsonBody(body);
        var response = await Client.ExecuteBulkWithErrorHandling<CreateObjectRecordResponse>(request);

        string createdEntityId = response.FirstOrDefault()?.Id ?? 
                                 throw new PluginApplicationException("Vault did not return the created record ID");
        var createdEntity = await Client.GetObjectRecord(objectIdentifier.VaultObjectName, createdEntityId);
        return new(createdEntity);
    }
}