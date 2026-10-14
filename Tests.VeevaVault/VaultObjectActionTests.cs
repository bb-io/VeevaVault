using Apps.VeevaVault.Actions;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Requests.VaultObject;
using Tests.VeevaVault.Base;

namespace Tests.VeevaVault;

[TestClass]
public class VaultObjectActionTests : TestBase
{
    [TestMethod]
    public async Task SearchObjectRecords_ReturnsRecords()
    {
        // Arrange
        var actions = new VaultObjectActions(InvocationContext);
        var objectIdentifier = new VaultObjectIdentifier { Name = "activity__v" };
        var searchInput = new SearchObjectRecordsRequest { NameContains = "" };

        // Act
        var result = await actions.SearchObjectRecords(objectIdentifier, searchInput);

        // Assert
        Assert.IsNotNull(result);
        PrintJsonResult(result);
    }
}
