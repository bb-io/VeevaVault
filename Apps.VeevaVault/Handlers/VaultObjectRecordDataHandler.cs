using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Helper;
using Apps.VeevaVault.Models.Entities.VaultObject;
using Apps.VeevaVault.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;

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

    // https://general.veevavault.dev/vault-api/api-reference/26.2/vault-objects/retrieve-object-records/
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        string vql = new VqlBuilder(_objectName, ["id", "name__v"])
            .Find(context.SearchString, "name__v")
            .Build();
        
        var response = await Client.Query<VaultObjectRecordMinimalEntity>(vql, maxPages: 1);
        return response.Select(x => new DataSourceItem(x.Id, x.Name)).ToList();
    }
}