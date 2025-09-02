using Domain.Entites;

namespace Application.Abstractions.Repositories;

public interface IMessageRepository : IRepositoryBase<Message>
{
    Task<IReadOnlyList<Message>> GetMessagesForUserAsync(Guid userId, string container);
    Task<IReadOnlyList<Message>> GetChatAsync(Guid currentUserId, Guid recipientId);
}
