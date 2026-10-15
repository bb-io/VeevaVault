namespace Apps.VeevaVault.Constants;

public static class VqlQueries
{
    public static string GetMinimalVaultObjectQuery(string vaultObjectName)
    {
        return $"SELECT id, name__v, created_date__v, modified_date__v FROM {vaultObjectName}";
    }
}