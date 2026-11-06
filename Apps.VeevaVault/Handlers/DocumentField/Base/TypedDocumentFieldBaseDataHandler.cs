using Apps.VeevaVault.Models.Entities.Document;
using Apps.VeevaVault.Models.Identifiers;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.VeevaVault.Handlers.DocumentField.Base;

public abstract class TypedDocumentFieldDataHandler(
    InvocationContext invocationContext,
    DocumentIdentifier documentIdentifier,
    params string[] fieldTypes) : DocumentFieldBaseDataHandler(invocationContext, documentIdentifier)
{
    protected override bool IsMatch(DocumentFieldEntity field) => fieldTypes.Contains(field.Type, StringComparer.OrdinalIgnoreCase);
}