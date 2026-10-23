using Apps.VeevaVault.Models.Entities.Document;
using Blackbird.Applications.Sdk.Common;

namespace Apps.VeevaVault.Models.Responses.Document;

public class DocumentResponse(DocumentEntity entity)
{
    [Display("Document ID")]
    public string Id { get; set; } = entity.Id.ToString();

    [Display("Document name")]
    public string Name { get; set; } = entity.Name;

    [Display("Document type")]
    public string Type { get; set; } = entity.Type;

    [Display("Document subtype")]
    public string Subtype { get; set; } = entity.Subtype;

    [Display("Document number")]
    public string DocumentNumber { get; set; } = entity.DocumentNumber;

    [Display("Document description")]
    public string? Description { get; set; } = entity.Description;

    [Display("Document status")]
    public string Status { get; set; } = entity.Status;

    [Display("Document created date")]
    public DateTime CreatedDate { get; set; } = entity.CreationDate;

    [Display("Document updated date")]
    public DateTime UpdatedDate { get; set; } = entity.UpdatedDate;
}