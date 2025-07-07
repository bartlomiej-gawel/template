using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgresUsername = builder.AddParameter("postgres-username", true);
var postgresPassword = builder.AddParameter("postgres-password", true);
var postgres = builder.AddPostgres("template-postgres", postgresUsername, postgresPassword)
    .WithDataVolume(isReadOnly: false)
    .WithLifetime(ContainerLifetime.Persistent);

builder.AddProject<Template_Services_Bootstrapper>("template-bootstrapper");
builder.AddProject<Template_Services_Gateway>("template-gateway");

builder.Build().Run();