using Application.Messages.Commands.SendMessage;
using FluentValidation;

namespace Application.Messages.Validators;

public sealed class SendMessageValidator : AbstractValidator<SendMessageCommand>
{
    public SendMessageValidator()
    {
        RuleFor(m => m.RecipientId).NotEmpty().WithMessage("RecipientId is required.");

        RuleFor(m => m.Content).NotEmpty().WithMessage("Content is required");
    }
}
