using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using MediatR;
using Shared;

namespace Application.Messages.Commands.EditMessage;

internal sealed class EditMessageHandler(IUnitOfWork unitOfWork,
    IUserContext userContext) : IRequestHandler<EditMessageCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(EditMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await unitOfWork.MessageRepository
            .GetAsync(m => m.SenderId == userContext.UserId && m.Id == request.Id);

        if (message is null)
        {
            return Result<Unit>.Failure("Message not found.");
        }

        var timeSinceCreated = DateTime.UtcNow - message.CreatedAt;

        if (timeSinceCreated > TimeSpan.FromMinutes(15))
        {
            return Result<Unit>.Failure("You can only edit a message within 15 minutes of sending it.");
        }

        if (request.Content == message.Content)
        {
            return Result<Unit>.Failure("No changes detected.");
        }

        message.Content = request.Content;

        message.UpdatedAt = DateTime.UtcNow;

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to edit message.");
    }
}
