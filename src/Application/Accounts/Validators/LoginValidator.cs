using Application.Accounts.Commands.Login;
using FluentValidation;

namespace Application.Accounts.Validators;

public sealed class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(lc => lc.Email).NotEmpty();

        RuleFor(lc => lc.Password).NotEmpty();
    }
}
