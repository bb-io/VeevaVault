using Microsoft.VisualBasic.FileIO;

namespace Apps.VeevaVault.Extensions;

public static class StringExtensions
{
    public static bool MatchesSearch(this string? source, string? search)
    {
        if (string.IsNullOrWhiteSpace(search))
            return true;

        return source?.Contains(search, StringComparison.OrdinalIgnoreCase) == true;
    }
    
    public static List<string> ParseJobErrors(this string csv)
    {
        using var parser = new TextFieldParser(new StringReader(csv));
        parser.TextFieldType = FieldType.Delimited;
        parser.HasFieldsEnclosedInQuotes = true;
        parser.SetDelimiters(",");
        parser.ReadFields(); // Skip header

        var errors = new List<string>();
        while (!parser.EndOfData)
        {
            var fields = parser.ReadFields();
            if (fields is not { Length: >= 3 })
                continue;

            string line = string.IsNullOrWhiteSpace(fields[1]) ? "" : $" ({fields[1]})";
            errors.Add($"{fields[0]}{line}: {fields[2]}");
        }

        return errors;
    }
}