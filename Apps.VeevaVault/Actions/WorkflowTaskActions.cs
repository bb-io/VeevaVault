using Apps.VeevaVault.Api;
using Apps.VeevaVault.Extensions;
using Apps.VeevaVault.Models.Entities.Workflow;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Requests.WorkflowTask;
using Apps.VeevaVault.Models.Responses.WorkflowTask;
using Apps.VeevaVault.Models.Utility.Wrapper;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.VeevaVault.Actions;

[ActionList("Workflow tasks")]
public class WorkflowTaskActions(InvocationContext invocationContext) : VeevaVaultInvocable(invocationContext)
{
    // https://general.veevavault.dev/vault-api/api-reference/26.2/workflows/workflow-tasks/retrieve-workflow-tasks/
    [Action("Search workflow tasks", Description = "Search workflow tasks with a specific criteria")]
    public async Task<SearchWorkflowTasksResponse> SearchWorkflowTasks(
        [ActionParameter] SearchWorkflowTasksRequest input)
    {
        var request = new VeevaRequest("objects/objectworkflows/tasks")
            .AddQueryParameter("assignee__v", string.IsNullOrWhiteSpace(input.AssigneeId) ? "me()" : input.AssigneeId);
        
        var entities = await Client.PaginateOffset<WorkflowTaskEntity>(request);

        if (!string.IsNullOrEmpty(input.LabelContains))
            entities = entities.Where(x => x.Label.MatchesSearch(input.LabelContains)).ToList();

        var response = entities.Select(x => new WorkflowTaskResponse(x)).ToList();
        return new(response);
    }
    
    // https://general.veevavault.dev/vault-api/api-reference/26.2/workflows/workflow-tasks/retrieve-workflow-task-details/
    [Action("Get workflow task", Description = "Get details for a specific workflow task")]
    public async Task<WorkflowTaskResponse> GetWorkflowTask(
        [ActionParameter] WorkflowTaskIdentifier workflowTaskIdentifier)
    {
        var request = new VeevaRequest($"objects/objectworkflows/tasks/{workflowTaskIdentifier.TaskId}");
        var response = await Client.ExecuteWithErrorHandling<DataArrayWrapper<WorkflowTaskEntity>>(request);
        return new(response.Data.First());
    }
}