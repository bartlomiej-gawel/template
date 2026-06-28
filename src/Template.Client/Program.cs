using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using Template.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddFluentUIComponents(options =>
{
    // The global overlay (DialogService.ShowGlobalOverlayAsync) is opt-in.
    // It defaults to enabled and renders a full-screen spinner on startup, so disable it.
    options.UseGlobalOverlay = false;
});

await builder.Build().RunAsync();
