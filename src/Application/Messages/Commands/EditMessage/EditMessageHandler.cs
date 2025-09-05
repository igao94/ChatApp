using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using Application.Helpers;
using MediatR;
using Shared;

namespace Application.Messages.Commands.EditMessage;

internal sealed class EditMessageHandler(IUnitOfWork unitOfWork,
    IUserContext userContext) : IRequestHandler<EditMessageCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(EditMessageCommand request, CancellationToken cancellationToken)
    {
        var messageResult = await unitOfWork.GetMessageBySenderAsync(request.Id, userContext.UserId);

        if (messageResult.IsFailure)
        {
            return Result<Unit>.Failure(messageResult.Error!);
        }

        var message = messageResult.Value!;

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

        return await unitOfWork.SaveChangesAsync()
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to edit message.");
    }
}
