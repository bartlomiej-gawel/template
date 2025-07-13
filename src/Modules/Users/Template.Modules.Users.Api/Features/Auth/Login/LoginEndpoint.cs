using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wolverine.Http;

namespace Template.Modules.Users.Api.Features.Auth.Login;

public static class LoginEndpoint
{
    [WolverinePost("auth/login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public static IResult Login(LoginRequest request)
    {
        var response = new LoginResponse(
            string.Empty,
            string.Empty);

        return Results.Ok(response);
    }
}