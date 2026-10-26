using Apps.VeevaVault.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.VeevaVault.Models.Identifiers.Optional;

public class DocumentVersionOptionalIdentifier
{
    [Display("Document version"), DataSource(typeof(DocumentVersionDataHandler))]
    public string? DocumentVersion { get; set; }
}