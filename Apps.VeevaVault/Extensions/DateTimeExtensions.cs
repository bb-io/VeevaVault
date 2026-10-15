namespace Apps.VeevaVault.Extensions;

public static class DateTimeExtensions
{
    public static string ToVeevaTimestampString(this DateTime dateTime)
    {
        return dateTime.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");
    }
}