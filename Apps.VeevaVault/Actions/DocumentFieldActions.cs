using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Responses.DocumentField;
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
    
    [Action("Get document number field value", Description = "Get a value of a number field for a specific document")]
    public async Task<GetNumberFieldValueResponse> GetNumberFieldValue(
        [ActionParameter] DocumentIdentifier documentIdentifier, 
        [ActionParameter] NumberDocumentFieldIdentifier fieldIdentifier)
    {
        var value = await Client.GetDocumentFieldValue(documentIdentifier.DocumentId, fieldIdentifier.FieldName);
        return new(value?.ToObject<decimal?>());
    }
    
    [Action("Get document boolean field value", Description = "Get a value of a boolean field for a specific document")]
    public async Task<GetBooleanFieldValueResponse> GetBooleanFieldValue(
        [ActionParameter] DocumentIdentifier documentIdentifier, 
        [ActionParameter] BooleanDocumentFieldIdentifier fieldIdentifier)
    {
        var value = await Client.GetDocumentFieldValue(documentIdentifier.DocumentId, fieldIdentifier.FieldName);
        return new(value?.ToObject<bool?>());
    }

    [Action("Get document date field value", Description = "Get a value of a date field for a specific document")]
    public async Task<GetDateFieldValueResponse> GetDateFieldValue(
        [ActionParameter] DocumentIdentifier documentIdentifier, 
        [ActionParameter] DateDocumentFieldIdentifier fieldIdentifier)
    {
        var value = await Client.GetDocumentFieldValue(documentIdentifier.DocumentId, fieldIdentifier.FieldName);
        return new(value?.ToObject<DateTime?>());
    }

    [Action("Get document picklist field values", Description = "Get selected values of a picklist field for a specific document")]
    public async Task<GetPicklistFieldValuesResponse> GetPicklistFieldValues(
        [ActionParameter] DocumentIdentifier documentIdentifier, 
        [ActionParameter] PicklistDocumentFieldIdentifier fieldIdentifier)
    {
        var value = await Client.GetDocumentFieldValue(documentIdentifier.DocumentId, fieldIdentifier.FieldName);
        return new(value?.ToObject<List<string>>() ?? []);
    }
}