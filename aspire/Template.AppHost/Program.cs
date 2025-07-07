using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Template_Services_Bootstrapper>("template-bootstrapper");
builder.AddProject<Template_Services_Gateway>("template-gateway");

builder.Build().Run();