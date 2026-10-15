using Apps.VeevaVault.Api;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Models.Entities.VaultObject;
using Apps.VeevaVault.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.VeevaVault.Handlers;

public class VaultObjectRecordDataHandler : VeevaVaultInvocable, IAsyncDataSourceItemHandler
{
    private readonly string _objectName;
    
    public VaultObjectRecordDataHandler(
        InvocationContext invocationContext,
        [ActionParameter] VaultObjectIdentifier objectIdentifier) : base(invocationContext)
    {
        if (string.IsNullOrEmpty(objectIdentifier.VaultObjectName))
            throw new PluginMisconfigurationException("Please specify vault object name first");

        _objectName = objectIdentifier.VaultObjectName;
    }

    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        string vql = $"SELECT id, name__v FROM {_objectName}";
        if (!string.IsNullOrEmpty(context.SearchString))
            vql += $" FIND ('{context.SearchString}' SCOPE name__v)";
        
        var request = new VeevaRequest("query", Method.Post).AddVql(vql);
        var response = await Client.Paginate<VaultObjectRecordMinimalEntity>(request);

        return response.Select(x => new DataSourceItem(x.Id, x.Name)).ToList();
    }
}