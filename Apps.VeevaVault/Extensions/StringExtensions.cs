namespace Apps.VeevaVault.Extensions;

public static class StringExtensions
{
    public static bool MatchesSearch(this string? source, string? search)
    {
        if (string.IsNullOrWhiteSpace(search))
            return true;

        return source?.Contains(search, StringComparison.OrdinalIgnoreCase) == true;
    }
}