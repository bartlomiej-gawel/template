using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Template.Client.Layout;

public sealed partial class MainLayout : LayoutComponentBase
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    private bool _isDarkMode;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _isDarkMode = await JsRuntime.InvokeAsync<bool>("isDarkModeEnabled");
            StateHasChanged();
        }
    }

    private async Task ToggleDarkMode()
    {
        await JsRuntime.InvokeVoidAsync("toggleDarkMode");
        _isDarkMode = !_isDarkMode;
    }
}
