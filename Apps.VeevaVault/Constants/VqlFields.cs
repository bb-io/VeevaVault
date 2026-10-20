namespace Apps.VeevaVault.Constants;

public static class VqlFields
{
    public static readonly string[] MinimalVaultObject = new[]{ "id", "name__v", "created_date__v", "modified_date__v" };

    public static readonly string[] MinimalDocument = new[] { "id", "name__v" };
    
    public static readonly string[] Document = new[] 
    { 
        "id", "name__v", "type__v", "size__v", "subtype__v", "document_number__v", 
        "description__v", "status__v", "document_creation_date__v", "version_modified_date__v"
    };
}