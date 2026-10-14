using Apps.VeevaVault.Handlers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Tests.VeevaVault.Base;

namespace Tests.VeevaVault;

[TestClass]
public class HandlerTests : TestBase
{
    [TestMethod]
    public async Task ObjectRecordDataHandler_ReturnsObjectRecords()
    {
        // Arrange
        var handler = new VaultObjectDataHandler(InvocationContext);

        // Act
        var result = await handler.GetDataAsync(new DataSourceContext { SearchString = "" }, CancellationToken.None);

        // Assert
        PrintDataHandlerResult(result);
        Assert.IsNotNull(result);
    }
}
