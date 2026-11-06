using Apps.VeevaVault.Api;
using Apps.VeevaVault.Extensions;

namespace Apps.VeevaVault.Helper;

public static class DocumentFilterHelper
{
    public static async Task<IReadOnlyDictionary<string, string>> GetFieldTypes(
        VeevaClient client, 
        List<(string Name, string Value)> fields)
    {
        if (fields.Count == 0)
            return new Dictionary<string, string>();

        var allFields = await client.GetAllDocumentFields();
        return allFields.ToDictionary(x => x.Name, x => x.Type);
    }
    
    public static async Task<List<long>?> GetDocumentIds(VeevaClient client, string? binderId, string? workflowStep)
    {
        List<long>? documentIds = null;

        if (!string.IsNullOrEmpty(binderId))
            documentIds = await client.GetBinderDocumentIds(binderId);

        if (!string.IsNullOrEmpty(workflowStep) && documentIds is not { Count: 0 })
        {
            var stepIds = await client.GetWorkflowStepDocumentIds(workflowStep);
            documentIds = documentIds is null ? stepIds : documentIds.Intersect(stepIds).ToList();
        }

        return documentIds;
    }
}