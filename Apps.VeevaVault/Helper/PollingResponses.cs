using Apps.VeevaVault.Models.Polling;
using Blackbird.Applications.Sdk.Common.Polling;

namespace Apps.VeevaVault.Helper;

public static class PollingResponses
{
    public static PollingEventResponse<DateMemory, T> DoNotFly<T>(DateTime pollingStart) where T : class
    {
        return new()
        {
            FlyBird = false,
            Memory = new DateMemory { LastPollingTime = pollingStart },
            Result = null
        };
    }
    
    public static PollingEventResponse<DateMemory, List<T>> FlyIfAny<T>(DateTime pollingStart, List<T> items)
    {
        return new()
        {
            FlyBird = items.Count > 0,
            Memory = new DateMemory { LastPollingTime = pollingStart },
            Result = items
        };
    }
}