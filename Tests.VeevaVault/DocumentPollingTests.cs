using Apps.VeevaVault.Events.Polling;
using Apps.VeevaVault.Events.Polling.Models.Requests.Document;
using Apps.VeevaVault.Models.Polling;
using Blackbird.Applications.Sdk.Common.Polling;
using Tests.VeevaVault.Base;

namespace Tests.VeevaVault;

[TestClass]
public class DocumentPollingTests : TestBase
{
    private DocumentPollingList Polling => new(InvocationContext);
    
    [TestMethod]
    public async Task OnDocumentCreatedOrUpdated_IsSuccess()
    {
        // Arrange
        var dateMemory = new DateMemory { LastPollingTime = DateTime.UtcNow.AddMinutes(-5) };
        var pollingRequest = new PollingEventRequest<DateMemory> { Memory = dateMemory };
        var input = new OnDocumentCreatedOrUpdatedRequest
        {
            
        };

        // Act
        var result = await Polling.OnDocumentCreatedOrUpdated(pollingRequest, input);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
}