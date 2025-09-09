using Application.Abstractions.Repositories;
using Domain.Entites;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class MessageRepository(AppDbContext context)
    : RepositoryBase<Message>(context), IMessageRepository
{
    public async Task MarkMessagesAsReadAsync(Guid currentUserId, Guid recipientId)
    {
        var unreadMessages = await _context.Messages
            .Where(m => m.RecipientId == currentUserId
                && m.SenderId == recipientId
                && m.DateRead == null)
            .ToListAsync();

        foreach (var message in unreadMessages)
        {
            message.DateRead = DateTime.UtcNow;
        }
    }

    public async Task<(IReadOnlyList<Message>, DateTime?)> GetChatAsync(Guid currentUserId,
        Guid recipientId,
        DateTime? cursor,
        int pageSize)
    {
        // In a production environment with SQL Server, I would use ExecuteUpdateAsync 
        // to mark messages as read directly in the database. 
        // However, since ExecuteUpdateAsync doesn't work with the in-memory database 
        // used for testing, this approach is commented out and handled via a separate endpoint instead.

        // await _context.Messages
        //    .Where(m => m.RecipientId == currentUserId
        //        && m.SenderId == recipientId
        //        && m.DateRead == null)
        //    .ExecuteUpdateAsync(setters => setters.SetProperty(m => m.DateRead, DateTime.UtcNow));

        var query = BuildMessageQuery(currentUserId, recipientId);

        query = query.OrderByDescending(m => m.CreatedAt);

        return await PaginateByCursorDescAsync(query, pageSize, cursor);
    }

    public async Task<(IReadOnlyList<Message>, DateTime?)> GetMessagesForUserAsync(Guid userId, 
        string? container,
        int pageSize,
        DateTime? cursor)
    {
        var query = _context.Messages
            .IgnoreQueryFilters()
            .AsNoTracking()
            .Include(m => m.Recipient)
            .Include(m => m.Sender)
            .AsQueryable();

        query = container switch
        {
            "Inbox" => query.Where(m => m.RecipientId == userId && !m.RecipientDeleted),
            "Outbox" => query.Where(m => m.SenderId == userId && !m.SenderDeleted),
            _ => query.Where(m => m.RecipientId == userId && !m.RecipientDeleted)
        };

        query = query.OrderByDescending(m => m.CreatedAt);

        return await PaginateByCursorDescAsync(query, pageSize, cursor);
    }

    public async Task<(IReadOnlyList<Message>, DateTime?)> SearchChatAsync(Guid currentUserId,
        Guid recipientId,
        int pageSize,
        DateTime? cursor,
        string searchTerm)
    {
        var query = BuildMessageQuery(currentUserId, recipientId);

        query = query.Where(m => m.Content.Contains(searchTerm)).OrderByDescending(m => m.CreatedAt);

        return await PaginateByCursorDescAsync(query, pageSize, cursor);
    }

    private IQueryable<Message> BuildMessageQuery(Guid currentUserId, Guid recipientId)
    {
        return _context.Messages
            .IgnoreQueryFilters()
            .AsNoTracking()
            .Include(m => m.Sender)
            .Include(m => m.Recipient)
            .Where(m => (m.RecipientId == currentUserId && m.SenderId == recipientId && !m.RecipientDeleted)
                || (m.SenderId == currentUserId && m.RecipientId == recipientId && !m.SenderDeleted))
            .AsQueryable();
    }
}
