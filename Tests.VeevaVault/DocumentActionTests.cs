using Apps.VeevaVault.Actions;
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
}