using Apps.VeevaVault.Events.Polling;
using Apps.VeevaVault.Models.Identifiers;
using Apps.VeevaVault.Models.Polling;
using Blackbird.Applications.Sdk.Common.Polling;
using Tests.VeevaVault.Base;

namespace Tests.VeevaVault;

[TestClass]
public class VaultObjectPollingTests : TestBase
{
    [TestMethod]
    public async Task OnObjectRecordCreatedOrUpdated_IsSuccess()
    {
        // Arrange
        var pollingList = new VaultObjectPollingList(InvocationContext);
        var memory = new DateMemory { LastPollingTime = DateTime.UtcNow - TimeSpan.FromMinutes(2) };
        var request = new PollingEventRequest<DateMemory> { Memory = memory };
        var objectIdentifier = new VaultObjectIdentifier { VaultObjectName = "product__v" };

        // Act
        var result = await pollingList.OnObjectRecordCreatedOrUpdated(request, objectIdentifier);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
}