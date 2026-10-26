using Apps.VeevaVault.Actions;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Requests.WorkflowTask;
using Tests.VeevaVault.Base;

namespace Tests.VeevaVault;

[TestClass]
public class WorkflowTaskActionTests : TestBase
{
    private WorkflowTaskActions Actions => new(InvocationContext);
    
    [TestMethod]
    public async Task SearchWorkflowTasks_ReturnsWorkflowTasks()
    {
        // Arrange
        var input = new SearchWorkflowTasksRequest
        {
            AssigneeId = "5695223"
        };

        // Act
        var result = await Actions.SearchWorkflowTasks(input);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public async Task GetWorkflowTask_ReturnsWorkflowTask()
    {
        // Arrange
        var taskId = new WorkflowTaskIdentifier { TaskId = "1201" };

        // Act
        var result = await Actions.GetWorkflowTask(taskId);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
}