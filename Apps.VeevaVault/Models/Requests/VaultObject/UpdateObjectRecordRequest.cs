using Apps.VeevaVault.Handlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.VeevaVault.Models.Requests.VaultObject;

public class UpdateObjectRecordRequest
{
    [Display("Vault object field name"), DataSource(typeof(VaultObjectFieldDataHandler))]
    public string FieldName { get; set; } = string.Empty;

    [Display("Vault object field value")]
    public string FieldValue { get; set; } = string.Empty;
}