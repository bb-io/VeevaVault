using Apps.VeevaVault.Handlers.DocumentField.Base;
using Apps.VeevaVault.Models.Entities.Document;
using Apps.VeevaVault.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.VeevaVault.Handlers.DocumentField;

public class UpdateDocumentFieldDataHandler(InvocationContext invocationContext, [ActionParameter] DocumentIdentifier documentIdentifier)
    : DocumentFieldBaseDataHandler(invocationContext, documentIdentifier)
{
    protected override bool IsMatch(DocumentFieldEntity field) => field.Editable;
}