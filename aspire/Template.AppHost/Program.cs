using Projects;
using Template.AppHost.Zitadel;

var builder = DistributedApplication.CreateBuilder(args);

var postgresUsername = builder.AddParameter("postgres-username", true);
var postgresPassword = builder.AddParameter("postgres-password", true);
var postgres = builder.AddPostgres("template-postgres", postgresUsername, postgresPassword)
    .WithDataVolume(isReadOnly: false)
    .WithLifetime(ContainerLifetime.Persistent);

var zitadelDb = postgres.AddDatabase("template-zitadel-db");
var zitadelUsername = builder.AddParameter("zitadel-username", true);
var zitadelPassword = builder.AddParameter("zitadel-password", true);
var zitadel = builder.AddZitadel("template-zitadel", zitadelDb, postgresUsername, postgresPassword, zitadelUsername, zitadelPassword)
    .WithReference(postgres)
    .WaitFor(postgres);

builder.AddProject<Template_Services_Bootstrapper>("template-services-bootstrapper")
    .WithReference(postgres)
    .WaitFor(postgres);

builder.AddProject<Template_Services_Gateway>("template-services-gateway")
    .WithReference(zitadel)
    .WaitFor(zitadel);

builder.Build().Run();