using Apps.VeevaVault.Api;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Models.Entities.VaultObject;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Requests.VaultObject;
using Apps.VeevaVault.Models.Responses.VaultObject;
using Apps.VeevaVault.Models.Utility.Wrapper;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
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
    [Action("Get object record")]
    public async Task<VaultObjectRecordResponse> GetObjectRecord(
        [ActionParameter] VaultObjectIdentifier objectIdentifier,
        [ActionParameter] VaultObjectRecordIdentifier recordIdentifier)
    {
        var request = new VeevaRequest($"vobjects/{objectIdentifier.VaultObjectName}/{recordIdentifier.VaultObjectRecordId}");
        var response = await Client.ExecuteWithErrorHandling<DataWrapper<VaultObjectRecordEntity>>(request);
        return new(response.Data);
    }
}