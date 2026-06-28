namespace Template.Client.Layout.Sidebar;

/// <summary>Shared open/closed state for the sidebar shell, cascaded to descendants.</summary>
public sealed class SidebarContext
{
    public SidebarContext(bool isOpen) => IsOpen = isOpen;

    public bool IsOpen { get; private set; }

    /// <summary>Raised when <see cref="IsOpen"/> actually changes.</summary>
    public event Action? OnChange;

    public void Open() => Set(true);

    public void Close() => Set(false);

    public void Toggle() => Set(!IsOpen);

    public void Set(bool open)
    {
        if (IsOpen == open)
        {
            return;
        }

        IsOpen = open;
        OnChange?.Invoke();
    }
}
