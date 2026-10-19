using Blackbird.Applications.Sdk.Common.Exceptions;

namespace Apps.VeevaVault.Helper;

public static class DocumentVersionHelper
{
    public static string ToVersionPath(string version)
    {
        var parts = version.Split('.');
        if (parts.Length != 2 || !int.TryParse(parts[0], out int major) || !int.TryParse(parts[1], out int minor))
            throw new PluginMisconfigurationException($"Version '{version}' is invalid. Use 'major.minor', e.g. '1.0'");

        return $"versions/{major}/{minor}";
    }
}