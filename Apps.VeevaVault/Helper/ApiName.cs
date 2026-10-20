namespace Apps.VeevaVault.Helper;

public static class ApiName
{
    public static string FromUrlValue(string value)
    {
        return value.TrimEnd('/').Split('/').Last();
    }
}