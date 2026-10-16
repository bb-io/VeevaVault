namespace Apps.VeevaVault.Helper;

public class VqlBuilder(string from, IEnumerable<string> fields)
{
    private readonly List<string> _conditions = [];
    private string? _find;
    
    public VqlBuilder WhereAfterOrEqual(string field, DateTime? value)
    {
        return value is null ? this : Where($"{field} >= '{value.Value.ToUniversalTime():yyyy-MM-dd'T'HH:mm:ss.fff'Z'}'");
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

    public string Build()
    {
        string vql = $"SELECT {string.Join(", ", fields)} FROM {from}";

        if (_find is not null)
            vql += $" {_find}";

        if (_conditions.Count != 0)
            vql += $" WHERE {string.Join("AND", _conditions)}";

        return vql;
    }
}