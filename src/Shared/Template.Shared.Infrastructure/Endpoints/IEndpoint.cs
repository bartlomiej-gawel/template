using Microsoft.AspNetCore.Routing;

namespace Template.Shared.Infrastructure.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}