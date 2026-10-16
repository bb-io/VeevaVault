using Apps.VeevaVault.Handlers;
using Apps.VeevaVault.Models.Identifiers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Tests.VeevaVault.Base;

namespace Tests.VeevaVault;

[TestClass]
public class HandlerTests : TestBase
{
    [TestMethod]
    public async Task VaultObjectDataHandler_ReturnsObjects()
    {
        // Arrange
        var handler = new VaultObjectDataHandler(InvocationContext);

        // Act
        var result = await handler.GetDataAsync(new DataSourceContext { SearchString = "" }, CancellationToken.None);

        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public async Task VaultObjectRecordDataHandler_ReturnsObjectRecords()
    {
        // Arrange
        var objectIdentifier = new VaultObjectIdentifier { VaultObjectName = "activity__v" };
        var handler = new VaultObjectRecordDataHandler(InvocationContext, objectIdentifier);

        // Act
        var result = await handler.GetDataAsync(new DataSourceContext { SearchString = "" }, CancellationToken.None);

        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public async Task VaultObjectFieldDataHandler_ReturnsObjectFields()
    {
        // Arrange
        var objectIdentifier = new VaultObjectIdentifier { VaultObjectName = "activity__v" };
        var handler = new VaultObjectFieldDataHandler(InvocationContext, objectIdentifier);

        // Act
        var result = await handler.GetDataAsync(new DataSourceContext { SearchString = "" }, CancellationToken.None);

        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public async Task DocumentTypeDataHandler_ReturnsDocuments()
    {
        // Arrange
        var handler = new DocumentTypeDataHandler(InvocationContext);

        // Act
        var result = await handler.GetDataAsync(new DataSourceContext { SearchString = "email" }, CancellationToken.None);

        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public async Task BinderDataHandler_ReturnsBinders()
    {
        // Arrange
        var handler = new BinderDataHandler(InvocationContext);

        // Act
        var result = await handler.GetDataAsync(new DataSourceContext { SearchString = "" }, CancellationToken.None);

        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public async Task WorkflowStepDataHandler_ReturnsWorkflowSteps()
    {
        // Arrange
        var handler = new WorkflowStepDataHandler(InvocationContext);

        // Act
        var result = await handler.GetDataAsync(new DataSourceContext { SearchString = "" }, CancellationToken.None);

        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }
}
