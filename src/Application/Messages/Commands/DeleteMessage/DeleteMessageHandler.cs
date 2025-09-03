using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using MediatR;
using Shared;

namespace Application.Messages.Commands.DeleteMessage;

internal sealed class DeleteMessageHandler(IUnitOfWork unitOfWork,
    IUserContext userContext) : IRequestHandler<DeleteMessageCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = userContext.UserId;

        var message = await unitOfWork.MessageRepository.GetByIdAsync(request.Id);

        if (message is null)
        {
            return Result<Unit>.Failure("Message not found.");
        }

        if (message.SenderId != currentUserId && message.RecipientId != currentUserId)
        {
            return Result<Unit>.Failure("You can't delete this message.");
        }

        if (message.SenderId == currentUserId)
        {
            message.SenderDeleted = true;
        }

        if (message.RecipientId == currentUserId)
        {
            message.RecipientDeleted = true;
        }

        if (message is { SenderDeleted: true, RecipientDeleted: true })
        {
            unitOfWork.MessageRepository.Delete(message);
        }

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete message.");
    }
}
