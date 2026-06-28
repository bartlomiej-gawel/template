namespace Template.Client.Layout.Sidebar;

/// <summary>Route helpers for deciding whether a nav category should auto-expand.</summary>
public static class SidebarRouteMatch
{
    /// <summary>
    /// True when <paramref name="relativePath"/> equals or is nested under
    /// <paramref name="basePath"/> (segment-aware, case-insensitive).
    /// </summary>
    public static bool IsUnder(string relativePath, string? basePath)
    {
        if (string.IsNullOrWhiteSpace(basePath))
        {
            return false;
        }

        var normalizedRelativePath = "/" + relativePath.Trim('/');
        var normalizedBasePath = "/" + basePath.Trim('/');

        return normalizedRelativePath.Equals(normalizedBasePath, StringComparison.OrdinalIgnoreCase) ||
               normalizedRelativePath.StartsWith(normalizedBasePath + "/", StringComparison.OrdinalIgnoreCase);
    }
}
