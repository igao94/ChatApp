using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using MediatR;
using Shared;

namespace Application.Messages.Commands.DeleteMessageForEveryone;

internal sealed class DeleteMessageForEveryoneHandler(IUnitOfWork unitOfWork,
    IUserContext userContext) : IRequestHandler<DeleteMessageForEveryoneCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteMessageForEveryoneCommand request,
        CancellationToken cancellationToken)
    {
        var message = await unitOfWork.MessageRepository
            .GetAsync(m => m.SenderId == userContext.UserId && m.Id == request.Id);

        if (message is null)
        {
            return Result<Unit>.Failure("Message not found.");
        }

        unitOfWork.MessageRepository.Delete(message);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete message.");
    }
}
