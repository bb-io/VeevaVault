using Apps.VeevaVault.Api;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Models.Entities.VaultObject;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Requests.VaultObject;
using Apps.VeevaVault.Models.Responses.VaultObject;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.VeevaVault.Actions;

[ActionList("Vault objects")]
public class VaultObjectActions(InvocationContext invocationContext) : VeevaVaultInvocable(invocationContext)
{
    [Action("Search object records", Description = "Search object records with a specific criteria")]
    public async Task<SearchVaultObjectRecordsResponse> SearchObjectRecords(
        [ActionParameter] VaultObjectIdentifier objectIdentifier,
        [ActionParameter] SearchObjectRecordsRequest searchInput)
    {
        string vql = $"SELECT id, name__v FROM {objectIdentifier.Name}";
        if (!string.IsNullOrEmpty(searchInput.NameContains))
            vql += $" FIND ('{searchInput.NameContains}' SCOPE name__v)";
        
        var request = new VeevaRequest("query", Method.Post).AddVql(vql);

        var response = await Client.Paginate<VaultObjectRecordEntity>(request);
        return new(response.Select(x => new VaultObjectRecordResponse(x)).ToList());
    }
}