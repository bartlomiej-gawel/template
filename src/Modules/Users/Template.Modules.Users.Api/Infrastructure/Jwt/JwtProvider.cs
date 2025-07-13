using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Template.Modules.Users.Api.Domain;

namespace Template.Modules.Users.Api.Infrastructure.Jwt;

public sealed class JwtProvider
{
    private const int AccessTokenExpirationMinutes = 10;
    private const int RefreshTokenExpirationMinutes = 30;

    private readonly IConfiguration _configuration;

    public JwtProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateAccessToken(User user)
    {
        var secretKey = _configuration["JwtSettings:SecretKey"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(AccessTokenExpirationMinutes),
            SigningCredentials = credentials,
            Issuer = _configuration["JwtSettings:Issuer"],
            Audience = _configuration["JwtSettings:Audience"]
        };

        var jwtHandler = new JsonWebTokenHandler();
        var accessToken = jwtHandler.CreateToken(tokenDescriptor);

        if (string.IsNullOrEmpty(accessToken))
            throw new InvalidOperationException("Failed to generate access token.");

        return accessToken;
    }

    public string GenerateRefreshToken()
    {
        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        if (string.IsNullOrEmpty(refreshToken))
            throw new InvalidOperationException("Failed to generate refresh token.");

        return refreshToken;
    }
}