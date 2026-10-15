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
        var objectIdentifier = new VaultObjectIdentifier { VaultObjectName = "activity__v" };
        var searchInput = new SearchObjectRecordsRequest { NameContains = "" };

        // Act
        var result = await actions.SearchObjectRecords(objectIdentifier, searchInput);

        // Assert
        Assert.IsNotNull(result);
        PrintJsonResult(result);
    }
    
    [TestMethod]
    public async Task GetObjectRecord_ReturnsRecord()
    {
        // Arrange
        var actions = new VaultObjectActions(InvocationContext);
        var objectIdentifier = new VaultObjectIdentifier { VaultObjectName = "activity__v" };
        var recordIdentifier = new VaultObjectRecordIdentifier { VaultObjectRecordId = "0AC000000002002" };

        // Act
        var result = await actions.GetObjectRecord(objectIdentifier, recordIdentifier);

        // Assert
        Assert.IsNotNull(result);
        PrintJsonResult(result);
    }
}
