using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using Application.Helpers;
using MediatR;
using Shared;

namespace Application.Messages.Commands.DeleteMessageForEveryone;

internal sealed class DeleteMessageForEveryoneHandler(IUnitOfWork unitOfWork,
    IUserContext userContext) : IRequestHandler<DeleteMessageForEveryoneCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteMessageForEveryoneCommand request,
        CancellationToken cancellationToken)
    {
        var messageResult = await unitOfWork.GetMessageBySenderAsync(request.Id, userContext.UserId);

        if (messageResult.IsFailure)
        {
            return Result<Unit>.Failure(messageResult.Error!);
        }

        var message = messageResult.Value!;

        unitOfWork.MessageRepository.Delete(message);

        return await unitOfWork.SaveChangesAsync()
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete message.");
    }
}
