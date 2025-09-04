using Application.Messages.Commands.EditMessage;
using FluentValidation;

namespace Application.Messages.Validators;

public sealed class EditMessageValidator : AbstractValidator<EditMessageCommand>
{
    public EditMessageValidator()
    {
        RuleFor(em => em.Id).NotEmpty().WithMessage("Id is required");

        RuleFor(em => em.Content).NotEmpty().WithMessage("Content is required");
    }
}
