using Apps.VeevaVault.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.VeevaVault.Models.Requests.Document;

public class SetDocumentRelationshipRequest
{
    [Display("Relationship type"), DataSource(typeof(DocumentRelationshipDataHandler))]
    public string RelationshipType { get; set; } = string.Empty;

    [Display("Target document ID"), DataSource(typeof(DocumentDataHandler))]
    public string TargetDocumentId { get; set; } = string.Empty;
}