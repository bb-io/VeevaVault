using Apps.VeevaVault.Actions;
using Apps.VeevaVault.Models.Identifiers;
using Tests.VeevaVault.Base;

namespace Tests.VeevaVault;

[TestClass]
public class DocumentFieldActionTests : TestBase
{
    private DocumentFieldActions Actions => new(InvocationContext);

    [TestMethod]
    public async Task GetTextFieldValue_ReturnsTextFieldValue()
    {
        // Arrange
        var documentIdentifier = new DocumentIdentifier { DocumentId = "2504" };
        var fieldIdentifier = new TextDocumentFieldIdentifier { FieldName = "status__v" };

        // Act
        var result = await Actions.GetTextFieldValue(documentIdentifier, fieldIdentifier);

        // Assert
        PrintJsonResult(result);
    }
}