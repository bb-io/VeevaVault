using Apps.VeevaVault.Api;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Models.Entities.Document;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Utility.Wrapper;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.VeevaVault.Handlers;

public class DocumentAttachmentDataHandler : VeevaVaultInvocable, IAsyncDataSourceItemHandler
{
    private readonly string _documentId;
    
    public DocumentAttachmentDataHandler(
        InvocationContext invocationContext, 
        [ActionParameter] DocumentIdentifier documentIdentifier) : base(invocationContext)
    {
        if (string.IsNullOrEmpty(documentIdentifier.DocumentId))
            throw new PluginMisconfigurationException("Please specify a document ID first");

        _documentId = documentIdentifier.DocumentId;
    }
    
    // https://general.veevavault.dev/vault-api/api-reference/26.2/documents/document-attachments/retrieve-document-attachments/
    public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken ct)
    {
        var request = new VeevaRequest($"objects/documents/{_documentId}/attachments");
        var response = await Client.ExecuteWithErrorHandling<DataArrayWrapper<DocumentAttachmentEntity>>(request);
        return response.Data
            .Where(x => x.Filename.MatchesSearch(context.SearchString))
            .Select(x => new DataSourceItem(x.Id.ToString(), x.Filename))
            .ToList();
    }
}