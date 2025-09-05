using Application.Abstractions.Repositories;
using Domain.Entites;
using Shared;

namespace Application.Helpers;

public static class GetMessageBySenderHelper
{
    public static async Task<Result<Message>> GetMessageBySenderAsync(this IUnitOfWork unitOfWork,
        Guid messageId,
        Guid userId)
    {
        var message = await unitOfWork.MessageRepository
            .GetAsync(m => m.Id == messageId && m.SenderId == userId);

        return message is not null
            ? Result<Message>.Success(message)
            : Result<Message>.Failure("Message not found.");
    }
}
