using FluentValidation;

namespace Template.Modules.Users.Api.Features.Auth.Login;

public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required to login.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required to login.");
    }
}