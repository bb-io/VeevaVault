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
    
    [TestMethod]
    public async Task GetNumberFieldValue_ReturnsNumberFieldValue()
    {
        // Arrange
        var documentIdentifier = new DocumentIdentifier { DocumentId = "2504" };
        var fieldIdentifier = new NumberDocumentFieldIdentifier { FieldName = "review_cycle_count__c" };

        // Act
        var result = await Actions.GetNumberFieldValue(documentIdentifier, fieldIdentifier);

        // Assert
        PrintJsonResult(result);
    }
    
    [TestMethod]
    public async Task GetBooleanFieldValue_ReturnsBooleanFieldValue()
    {
        // Arrange
        var documentIdentifier = new DocumentIdentifier { DocumentId = "2504" };
        var fieldIdentifier = new BooleanDocumentFieldIdentifier { FieldName = "public_content__v" };

        // Act
        var result = await Actions.GetBooleanFieldValue(documentIdentifier, fieldIdentifier);

        // Assert
        PrintJsonResult(result);
    }

    [TestMethod]
    public async Task GetDateFieldValue_ReturnsDateFieldValue()
    {
        // Arrange
        var documentIdentifier = new DocumentIdentifier { DocumentId = "2504" };
        var fieldIdentifier = new DateDocumentFieldIdentifier { FieldName = "start_date__c" };

        // Act
        var result = await Actions.GetDateFieldValue(documentIdentifier, fieldIdentifier);

        // Assert
        PrintJsonResult(result);
    }

    [TestMethod]
    public async Task GetPicklistFieldValues_ReturnsPicklistFieldValues()
    {
        // Arrange
        var documentIdentifier = new DocumentIdentifier { DocumentId = "2504" };
        var fieldIdentifier = new PicklistDocumentFieldIdentifier { FieldName = "target__c" };

        // Act
        var result = await Actions.GetPicklistFieldValues(documentIdentifier, fieldIdentifier);

        // Assert
        PrintJsonResult(result);
    }
}