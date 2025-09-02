using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using MediatR;
using Shared;

namespace Application.Messages.Commands.MarkMessagesAsRead;

internal sealed class MarkMessagesAsReadHandler(IUnitOfWork unitOfWork,
    IUserContext userContext) : IRequestHandler<MarkMessagesAsReadCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(MarkMessagesAsReadCommand request,
        CancellationToken cancellationToken)
    {
        await unitOfWork.MessageRepository.MarkMessagesAsReadAsync(userContext.UserId, request.RecipientId);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to mark messages as read.");
    }
}
