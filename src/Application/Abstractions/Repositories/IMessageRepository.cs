using Domain.Entites;

namespace Application.Abstractions.Repositories;

public interface IMessageRepository : IRepositoryBase<Message>
{
    Task<(IReadOnlyList<Message>, DateTime?)> GetMessagesForUserAsync(Guid userId,
        string? container,
        int pageSize,
        DateTime? cursor);

    Task<(IReadOnlyList<Message>, DateTime?)> GetChatAsync(Guid currentUserId,
        Guid recipientId,
        DateTime? cursor,
        int pageSize);

    Task MarkMessagesAsReadAsync(Guid currentUserId, Guid recipientId);

    Task<(IReadOnlyList<Message>, DateTime?)> SearchChatAsync(Guid currentUserId,
       Guid recipientId,
       int pageSize,
       DateTime? cursor,
       string searchTerm);
}
