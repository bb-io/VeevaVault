using System.Globalization;
using Apps.VeevaVault.Extensions;
using Blackbird.Applications.Sdk.Common.Exceptions;

namespace Apps.VeevaVault.Helper;

public class VqlBuilder(string from, IEnumerable<string> fields)
{
    private readonly List<string> _conditions = [];
    private string? _find;
    
    public VqlBuilder WhereAfterOrEqual(string field, DateTime? value)
    {
        return value is null ? this : Where($"{field} >= '{value.Value.ToUniversalTime():yyyy-MM-dd'T'HH:mm:ss.fff'Z'}'");
    }

    public VqlBuilder WhereContains(string field, IEnumerable<string>? values)
    {
        var quoted = values?
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .Select(v => $"'{v}'")
            .ToList();

        return quoted is null || quoted.Count == 0 
            ? this 
            : Where($"{field} CONTAINS ({string.Join(", ", quoted)})");
    }

    public VqlBuilder WhereContains(string field, IEnumerable<long>? values)
    {
        return values is null || values.Count() == 0 
            ? this 
            : Where($"{field} CONTAINS ({string.Join(", ", values)})");
    }
    
    public VqlBuilder WhereEquals(string field, long? value)
    {
        return value is null ? this : Where($"{field} = {value}");
    }
    
    public VqlBuilder WhereEquals(string field, string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? this : Where($"{field} = '{value}'");
    }
    
    public VqlBuilder Where(string condition)
    {
        _conditions.Add(condition);
        return this;
    }

    public VqlBuilder Find(string? phrase, string scope)
    {
        if (!string.IsNullOrWhiteSpace(phrase))
            _find = $"FIND ('{phrase}' SCOPE {scope})";

        return this;
    }
    
    public VqlBuilder WhereFieldsEqual(IEnumerable<(string Name, string Value)> fields, IReadOnlyDictionary<string, string> fieldTypes)
    {
        foreach (var (name, value) in fields)
        {
            if (!fieldTypes.TryGetValue(name, out var type))
                throw new PluginMisconfigurationException($"Document field '{name}' does not exist");

            Where($"{name} = {FormatValue(name, type, value)}");
        }

        return this;
    }

    public string Build()
    {
        string vql = $"SELECT {string.Join(", ", fields)} FROM {from}";

        if (_find is not null)
            vql += $" {_find}";

        if (_conditions.Count != 0)
            vql += $" WHERE {string.Join(" AND ", _conditions)}";

        return vql;
    }

    private static string FormatValue(string name, string type, string value)
    {
        return type.ToLowerInvariant() switch
        {
            "number" => decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var number)
                ? number.ToString(CultureInfo.InvariantCulture)
                : throw new PluginMisconfigurationException($"Value '{value}' of field '{name}' is not a valid number"),
            "boolean" => bool.TryParse(value, out var flag)
                ? flag.ToString().ToLowerInvariant()
                : throw new PluginMisconfigurationException($"Value '{value}' of field '{name}' is not a valid boolean"),
            _ => value.Quote()
        };
    }
}