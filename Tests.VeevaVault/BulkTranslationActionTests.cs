using Apps.VeevaVault.Actions;
using Apps.VeevaVault.Constants;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Requests.BulkTranslation;
using Blackbird.Applications.Sdk.Common.Files;
using Tests.VeevaVault.Base;

namespace Tests.VeevaVault;

[TestClass]
public class BulkTranslationActionTests : TestBase
{
    private BulkTranslationActions Actions => new(InvocationContext, FileManager);

    [TestMethod]
    public async Task ExportBulkTranslationFile_IsSuccess()
    {
        // Arrange
        var language = new LanguageIdentifier { LanguageKey = "uk" };
        var messageType = new MessageTypeIdentifier { MessageType = MessageType.SystemMessages };

        // Act
        var result = await Actions.ExportBulkTranslationsFile(language, messageType);

        // Assert
        Console.WriteLine(result.File.Name);
        Assert.IsNotNull(result.File);
    }
    
    [TestMethod]
    public async Task ImportBulkTranslationsFile_ReturnsImportResult()
    {
        // Arrange
        var messageType = new MessageTypeIdentifier { MessageType = MessageType.SystemMessages };
        var input = new ImportBulkTranslationsFileRequest { File = new FileReference { Name = "test.csv" } };

        // Act
        var result = await Actions.ImportBulkTranslationsFile(messageType, input);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
}