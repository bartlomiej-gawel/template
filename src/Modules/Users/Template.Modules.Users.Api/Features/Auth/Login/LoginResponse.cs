namespace Template.Modules.Users.Api.Features.Auth.Login;

public sealed record LoginResponse(
    string AccessToken,
    string RefreshToken);