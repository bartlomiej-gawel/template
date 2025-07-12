using Template.Aspire.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
// builder.Services.AddInfrastructure();

// var assemblies = ModuleLoader.LoadAssemblies();
// var modules = ModuleLoader.LoadModules(assemblies);

// foreach (var module in modules)
//     module.Register(builder.Services, builder.Configuration);

// builder.Services.AddEndpoints(assemblies);

var app = builder.Build();
app.UseServiceDefaults();
// app.UseInfrastructure();

// foreach (var module in modules)
//     module.Use(app);

// app.MapEndpoints();

// assemblies.Clear();
// modules.Clear();

app.Run();