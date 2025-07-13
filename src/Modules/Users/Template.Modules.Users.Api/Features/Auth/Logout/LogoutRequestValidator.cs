using FluentValidation;

namespace Template.Modules.Users.Api.Features.Auth.Logout;

public sealed class LogoutRequestValidator : AbstractValidator<LogoutRequest>
{
    public LogoutRequestValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh token is required to logout.");
    }
}