using Template.Aspire.ServiceDefaults;
using Template.Shared.Infrastructure;
using Template.Shared.Infrastructure.Modules;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddInfrastructure();

var assemblies = ModuleLoader.LoadAssemblies();
var modules = ModuleLoader.LoadModules(assemblies);

foreach (var module in modules)
    module.Register(builder.Services, builder.Configuration);

var app = builder.Build();
app.UseServiceDefaults();
app.UseInfrastructure();

foreach (var module in modules)
    module.Use(app);

assemblies.Clear();
modules.Clear();

app.Run();