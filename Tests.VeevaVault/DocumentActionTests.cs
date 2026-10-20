using Apps.VeevaVault.Actions;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Requests.Document;
using Blackbird.Applications.Sdk.Common.Files;
using Tests.VeevaVault.Base;

namespace Tests.VeevaVault;

[TestClass]
public class DocumentActionTests : TestBase
{
    private DocumentActions Actions => new(InvocationContext, FileManager);
    
    [TestMethod]
    public async Task SearchDocuments_ReturnsDocuments()
    {
        // Arrange
        var input = new SearchDocumentsRequest
        {
            NameContains = "",
        };

        // Act
        var result = await Actions.SearchDocuments(input);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public async Task GetDocument_ReturnsDocument()
    {
        // Arrange
        var input = new DocumentIdentifier { DocumentId = "2504" };

        // Act
        var result = await Actions.GetDocument(input);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task UpdateDocument_IsSuccess()
    {
        // Arrange
        var identifier = new DocumentIdentifier { DocumentId = "2504" };

        string updatedFieldValue = "test2";
        var input = new UpdateDocumentRequest
        {
            DocumentFieldNames =  ["name__v"],
            DocumentFieldValues = [updatedFieldValue]
        };

        // Act
        await Actions.UpdateDocument(identifier, input);
    }

    [TestMethod]
    public async Task DownloadDocumentAttachment_IsSuccess()
    {
        // Arrange
        var documentIdentifier = new DocumentIdentifier { DocumentId = "2504" };
        var attachmentIdentifier = new DocumentAttachmentIdentifier { AttachmentId = "2701" };

        // Act
        var result = await Actions.DownloadDocumentAttachment(documentIdentifier, attachmentIdentifier);

        // Assert
        Console.WriteLine(result.File.Name);
        Console.WriteLine(result.File.ContentType);
        Assert.IsNotNull(result.File);
    }

    [TestMethod]
    public async Task DownloadDocument_IsSuccess()
    {
        // Arrange
        var documentIdentifier = new DocumentIdentifier { DocumentId = "2504" };
        var input = new DownloadDocumentRequest
        {
            Version = "0.1"
        };

        // Act
        var result = await Actions.DownloadDocument(documentIdentifier, input);

        // Assert
        Console.WriteLine(result.File.Name);
        Console.WriteLine(result.File.ContentType);
        Assert.IsNotNull(result.File);
    }

    [TestMethod]
    public async Task UploadDocumentAttachment_IsSuccess()
    {
        // Arrange
        var documentIdentifier = new DocumentIdentifier { DocumentId = "2504" };
        var input = new UploadDocumentAttachmentRequest { File = new FileReference { Name = "hello.txt" } };

        // Act
        var result = await Actions.UploadDocumentAttachment(documentIdentifier, input);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task CreateDocument_ReturnsCreatedDocument()
    {
        // Arrange
        var documentIdentifier = new DocumentTypeIdentifier { DocumentType = "publication__c" };
        var input = new CreateDocumentRequest
        {
            File = new FileReference { Name = "hello.txt" },
            Lifecycle = "publication__c",
            Subtype = "poster__c",
        };

        // Act
        var result = await Actions.CreateDocument(documentIdentifier, input);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
}