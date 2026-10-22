using Apps.VeevaVault.Actions;
using Apps.VeevaVault.Constants;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Requests.BulkTranslation;
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
        var language = new LanguageIdentifier { LanguageKey = "en" };
        var input = new ExportBulkTranslationFileRequest { MessageType = MessageType.SystemMessages };

        // Act
        var result = await Actions.ExportBulkTranslationFile(language, input);

        // Assert
        Console.WriteLine(result.File.Name);
        Assert.IsNotNull(result.File);
    }
}