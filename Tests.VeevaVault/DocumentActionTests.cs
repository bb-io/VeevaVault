using Apps.VeevaVault.Actions;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Requests.Document;
using Tests.VeevaVault.Base;

namespace Tests.VeevaVault;

[TestClass]
public class DocumentActionTests : TestBase
{
    [TestMethod]
    public async Task SearchDocuments_ReturnsDocuments()
    {
        // Arrange
        var actions = new DocumentActions(InvocationContext);
        var input = new SearchDocumentsRequest
        {
            NameContains = "",
        };

        // Act
        var result = await actions.SearchDocuments(input);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public async Task GetDocument_ReturnsDocument()
    {
        // Arrange
        var actions = new DocumentActions(InvocationContext);
        var input = new DocumentIdentifier { DocumentId = "2504" };

        // Act
        var result = await actions.GetDocument(input);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task UpdateDocument_IsSuccess()
    {
        // Arrange
        var actions = new DocumentActions(InvocationContext);
        var identifier = new DocumentIdentifier { DocumentId = "2504" };

        string updatedFieldValue = "test2";
        var input = new UpdateDocumentRequest
        {
            DocumentFieldNames =  ["name__v"],
            DocumentFieldValues = [updatedFieldValue]
        };

        // Act
        await actions.UpdateDocument(identifier, input);
    }
}