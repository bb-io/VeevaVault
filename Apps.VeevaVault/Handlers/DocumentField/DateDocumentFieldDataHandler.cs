using Apps.VeevaVault.Handlers.DocumentField.Base;
using Apps.VeevaVault.Models.Identifiers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.VeevaVault.Handlers.DocumentField;

public class DateDocumentFieldDataHandler(InvocationContext invocationContext, [ActionParameter] DocumentIdentifier documentIdentifier)
    : TypedDocumentFieldDataHandler(invocationContext, documentIdentifier, "Date", "DateTime");
