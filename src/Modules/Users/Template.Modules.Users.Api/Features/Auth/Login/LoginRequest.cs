namespace Template.Modules.Users.Api.Features.Auth.Login;

public sealed record LoginRequest(
    string Email,
    string Password);