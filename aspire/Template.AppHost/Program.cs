using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Template_Bootstrapper>("template-bootstrapper");

builder.Build().Run();