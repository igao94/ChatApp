using Application.Accounts.Commands.Register;
using FluentValidation;

namespace Application.Accounts.Validators;

public sealed class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(rc => rc.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(15).WithMessage("Name cannot be longer than 15 characters.")
            .Matches(@"^[A-Z]").WithMessage("Name must start with an uppercase letter.");

        RuleFor(rc => rc.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Please enter a valid email address.");

        RuleFor(rc => rc.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
            .MaximumLength(20).WithMessage("Password cannot exceed 20 characters.")
            .Matches(@"[\W_]").WithMessage("Password must contain at least one non-alphanumeric character.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one digit.");
    }
}
