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
        var result = await handler.GetDataAsync(new DataSourceContext { SearchString = "" }, CancellationToken.None);

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
    
    [TestMethod]
    public async Task DocumentDataHandler_ReturnsDocuments()
    {
        // Arrange
        var handler = new DocumentDataHandler(InvocationContext);

        // Act
        var result = await handler.GetDataAsync(new DataSourceContext { SearchString = "" }, CancellationToken.None);

        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public async Task DocumentFieldDataHandler_ReturnsFields()
    {
        // Arrange
        var documentIdentifier = new DocumentIdentifier { DocumentId = "2504" };
        var handler = new DocumentFieldDataHandler(InvocationContext, documentIdentifier);
    
        // Act
        var result = await handler.GetDataAsync(new DataSourceContext { SearchString = "" }, CancellationToken.None);
    
        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public async Task DocumentAttachmentDataHandler_ReturnsAttachments()
    {
        // Arrange
        var documentIdentifier = new DocumentIdentifier { DocumentId = "2504" };
        var handler = new DocumentAttachmentDataHandler(InvocationContext, documentIdentifier);
    
        // Act
        var result = await handler.GetDataAsync(new DataSourceContext { SearchString = "" }, CancellationToken.None);
    
        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public async Task DocumentVersionDataHandler_ReturnsVersions()
    {
        // Arrange
        var documentIdentifier = new DocumentIdentifier { DocumentId = "2504" };
        var handler = new DocumentVersionDataHandler(InvocationContext, documentIdentifier);
    
        // Act
        var result = await handler.GetDataAsync(new DataSourceContext { SearchString = "" }, CancellationToken.None);
    
        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public async Task DocumentTypeLifecycleDataHandler_ReturnsLifecycles()
    {
        // Arrange
        var documentTypeIdentifier = new DocumentTypeIdentifier { DocumentType = "publication__c" };
        var handler = new DocumentTypeLifecycleDataHandler(InvocationContext, documentTypeIdentifier);
    
        // Act
        var result = await handler.GetDataAsync(new DataSourceContext { SearchString = "" }, CancellationToken.None);
    
        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public async Task DocumentSubtypeDataHandler_ReturnsSubtypes()
    {
        // Arrange
        var documentTypeIdentifier = new DocumentTypeIdentifier { DocumentType = "publication__c" };
        var handler = new DocumentSubtypeDataHandler(InvocationContext, documentTypeIdentifier);
    
        // Act
        var result = await handler.GetDataAsync(new DataSourceContext { SearchString = "" }, CancellationToken.None);
    
        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public async Task DocumentRelationshipDataHandler_ReturnsRelationships()
    {
        // Arrange
        var documentIdentifier = new DocumentIdentifier { DocumentId = "2504" };
        var handler = new DocumentRelationshipDataHandler(InvocationContext, documentIdentifier);
    
        // Act
        var result = await handler.GetDataAsync(new DataSourceContext { SearchString = "" }, CancellationToken.None);
    
        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public async Task DocumentUserActionDataHandler_ReturnsUserActions()
    {
        // Arrange
        var documentIdentifier = new DocumentIdentifier { DocumentId = "2802" };
        var documentVersionIdentifier = new DocumentVersionIdentifier { DocumentVersion = "0.1" };
        var handler = new DocumentUserActionDataHandler(InvocationContext, documentIdentifier, documentVersionIdentifier);
    
        // Act
        var result = await handler.GetDataAsync(new DataSourceContext { SearchString = "" }, CancellationToken.None);
    
        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }
}
