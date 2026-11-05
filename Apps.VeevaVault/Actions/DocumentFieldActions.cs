using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Responses.Document;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.VeevaVault.Actions;

[ActionList("Document fields")]
public class DocumentFieldActions(InvocationContext invocationContext) : VeevaVaultInvocable(invocationContext)
{
    [Action("Get document text field value", Description = "Get a value of a text field for a specific document")]
    public async Task<GetTextFieldValueResponse> GetTextFieldValue(
        [ActionParameter] DocumentIdentifier documentIdentifier, 
        [ActionParameter] TextDocumentFieldIdentifier fieldIdentifier)
    {
        var value = await Client.GetDocumentFieldValue(documentIdentifier.DocumentId, fieldIdentifier.FieldName);
        return new(value?.ToString());
    }
}