using Apps.VeevaVault.Models.Responses.Document.Api;
using Blackbird.Applications.Sdk.Common;

namespace Apps.VeevaVault.Models.Responses.Document;

public class SetDocumentRelationshipResponse(CreateSingleDocumentRelationshipResponse response)
{
    [Display("Relationship ID")]
    public string RelationshipId { get; set; } = response.Id.ToString();
}