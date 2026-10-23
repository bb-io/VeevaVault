using Apps.VeevaVault.Constants;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.VeevaVault.Handlers.Static;

public class MessageTypeStaticDataHandler : IStaticDataSourceItemHandler
{
    // https://general.veevavault.dev/vault-api/api-reference/26.2/bulk-translation/export-bulk-translation-file/#uri-path-parameters
    public IEnumerable<DataSourceItem> GetData()
    {
        return
        [
            new DataSourceItem(MessageType.FieldLabels, "Field labels"),
            new DataSourceItem(MessageType.SystemMessages, "System messages"),
            new DataSourceItem(MessageType.NotificationTemplateMessages, "Notification template messages"),
            new DataSourceItem(MessageType.UserAccountMessages, "User account messages"),
        ];
    }
}