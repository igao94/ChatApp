using Domain.Entites;

namespace Application.Abstractions.Repositories;

public interface IMessageRepository : IRepositoryBase<Message>
{
    Task<IReadOnlyList<Message>> GetMessagesForUserAsync(Guid userId, string container);
    Task<(IReadOnlyList<Message>, DateTime?)> GetChatAsync(Guid currentUserId,
        Guid recipientId,
        DateTime? cursor,
        int pageSize);
    Task MarkMessagesAsReadAsync(Guid currentUserId, Guid recipientId);
    Task NullifyUserIdsInMessagesAsync(Guid userId);
    Task<IReadOnlyList<Message>> SearchChatAsync(Guid currentUserId, Guid recipientId, string? searchTerm);
}
