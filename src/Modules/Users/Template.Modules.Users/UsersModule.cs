using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Shared.Infrastructure.Endpoints;
using Template.Shared.Infrastructure.Modules;

namespace Template.Modules.Users;

public sealed class UsersModule : IModule
{
    public string Name => "Users";

    public void Register(IServiceCollection services, IConfiguration configuration)
    {
    }

    public void Use(WebApplication app)
    {
    }
}