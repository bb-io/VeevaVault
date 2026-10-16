using Apps.VeevaVault.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.VeevaVault.Models.Requests.Document;

public class SearchDocumentsRequest
{
    [Display("Document name contains")]
    public string? NameContains { get; set; }

    [Display("Document types"), DataSource(typeof(DocumentTypeDataHandler))]
    public List<string>? DocumentTypes { get; set; }

    [Display("Document binder ID"), DataSource(typeof(BinderDataHandler))]
    public string? BinderId { get; set; }

    [Display("Workflow step name"), DataSource(typeof(WorkflowStepDataHandler))]
    public string? WorkflowStepName { get; set; }
}