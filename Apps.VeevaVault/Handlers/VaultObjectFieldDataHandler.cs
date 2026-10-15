using Apps.VeevaVault.Api;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Responses.VaultObject.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.VeevaVault.Handlers;

public class VaultObjectFieldDataHandler : VeevaVaultInvocable, IAsyncDataSourceItemHandler
{
    private readonly string _objectName;
    
    public VaultObjectFieldDataHandler(
        InvocationContext invocationContext,
        [ActionParameter] VaultObjectIdentifier objectIdentifier) : base(invocationContext)
    {
        if (string.IsNullOrEmpty(objectIdentifier.VaultObjectName))
            throw new PluginMisconfigurationException("Please specify vault object name first");

        _objectName = objectIdentifier.VaultObjectName;
    }

    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        var request = new VeevaRequest($"metadata/vobjects/{_objectName}");
        var response = await Client.ExecuteWithErrorHandling<RetrieveObjectMetadataResponse>(request);

        var fields = response.Object.Fields;
        return fields
            .Where(x => x.IsEditable)
            .Where(x =>
                string.IsNullOrEmpty(context.SearchString) ||
                x.Label.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .Select(x => new DataSourceItem(x.Name, x.Label))
            .ToList();
    }
}