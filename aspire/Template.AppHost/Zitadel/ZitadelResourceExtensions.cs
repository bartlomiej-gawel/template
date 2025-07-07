namespace Template.AppHost.Zitadel;

internal static class ZitadelResourceExtensions
{
    public static IResourceBuilder<ZitadelResource> AddZitadel(
        this IDistributedApplicationBuilder builder,
        string name,
        IResourceBuilder<PostgresDatabaseResource> postgresDatabase,
        IResourceBuilder<ParameterResource> postgresUsername,
        IResourceBuilder<ParameterResource> postgresPassword,
        IResourceBuilder<ParameterResource> zitadelUsername,
        IResourceBuilder<ParameterResource> zitadelPassword,
        string? masterKey = null)
    {
        var zitadelMasterKey = masterKey
                               ?? builder.Configuration["Zitadel:MasterKey"]
                               ?? "MasterkeyNeedsToHave32Characters";

        var resource = new ZitadelResource(name);

        return builder.AddResource(resource)
            .WithImage("ghcr.io/zitadel/zitadel", "latest")
            .WithHttpEndpoint(targetPort: 8080, name: "http")
            .WithEnvironment(context =>
            {
                var postgres = postgresDatabase.Resource.Parent;
                if (postgres == null)
                    throw new InvalidOperationException("Database resource must have a PostgreSQL server parent");

                context.EnvironmentVariables["ZITADEL_DATABASE_POSTGRES_HOST"] = postgres.Name;
                context.EnvironmentVariables["ZITADEL_DATABASE_POSTGRES_PORT"] = "5432";
                context.EnvironmentVariables["ZITADEL_DATABASE_POSTGRES_DATABASE"] = postgresDatabase.Resource.DatabaseName;
                context.EnvironmentVariables["ZITADEL_DATABASE_POSTGRES_USER_USERNAME"] = "zitadel";
                context.EnvironmentVariables["ZITADEL_DATABASE_POSTGRES_USER_PASSWORD"] = "zitadel";
                context.EnvironmentVariables["ZITADEL_DATABASE_POSTGRES_USER_SSL_MODE"] = "disable";
                context.EnvironmentVariables["ZITADEL_DATABASE_POSTGRES_ADMIN_USERNAME"] = postgresUsername.Resource;
                context.EnvironmentVariables["ZITADEL_DATABASE_POSTGRES_ADMIN_PASSWORD"] = postgresPassword.Resource;
                context.EnvironmentVariables["ZITADEL_DATABASE_POSTGRES_ADMIN_SSL_MODE"] = "disable";
                context.EnvironmentVariables["ZITADEL_EXTERNALSECURE"] = "false";
                context.EnvironmentVariables["ZITADEL_FIRSTINSTANCE_ORG_HUMAN_USERNAME"] = zitadelUsername.Resource;
                context.EnvironmentVariables["ZITADEL_FIRSTINSTANCE_ORG_HUMAN_PASSWORD"] = zitadelPassword.Resource;
            })
            .WithArgs("start-from-init", "--masterkey", zitadelMasterKey, "--tlsMode", "disabled")
            .WithLifetime(ContainerLifetime.Persistent)
            .WaitFor(postgresDatabase);
    }

    public static IResourceBuilder<TDestination> WithReference<TDestination>(
        this IResourceBuilder<TDestination> builder,
        IResourceBuilder<ZitadelResource> zitadel,
        string? connectionName = null)
        where TDestination : IResourceWithEnvironment
    {
        var connectionStringName = connectionName ?? "Zitadel__Authority";
        return builder.WithEnvironment(connectionStringName, zitadel.Resource.ConnectionStringExpression);
    }
}