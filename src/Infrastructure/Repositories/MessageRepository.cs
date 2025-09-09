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

        return await PaginateByCursorDescAsync(query, pageSize, cursor);
    }

    public async Task<IReadOnlyList<Message>> GetMessagesForUserAsync(Guid userId, string container)
    {
        var query = _context.Messages
            .IgnoreQueryFilters()
            .AsNoTracking()
            .Include(m => m.Recipient)
            .Include(m => m.Sender)
            .OrderByDescending(m => m.CreatedAt)
            .AsQueryable();

        query = container switch
        {
            "Inbox" => query.Where(m => m.RecipientId == userId && !m.RecipientDeleted),
            "Outbox" => query.Where(m => m.SenderId == userId && !m.SenderDeleted),
            _ => query.Where(m => m.RecipientId == userId && !m.RecipientDeleted)
        };

        return await query.ToListAsync();
    }

    public async Task NullifyUserIdsInMessagesAsync(Guid userId)
    {
        await _context.Messages
            .Where(m => m.SenderId == userId)
            .ExecuteUpdateAsync(setters => setters.SetProperty(m => m.SenderId, (Guid?)null));

        await _context.Messages
            .Where(m => m.RecipientId == userId)
            .ExecuteUpdateAsync(setters => setters.SetProperty(m => m.RecipientId, (Guid?)null));
    }

    public async Task<IReadOnlyList<Message>> SearchChatAsync(Guid currentUserId,
        Guid recipientId,
        string? searchTerm)
    {
        var query = BuildMessageQuery(currentUserId, recipientId);

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(m => m.Content.ToLower().Contains(searchTerm.ToLower()));
        }

        return await query.ToListAsync();
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
            .OrderByDescending(m => m.CreatedAt)
            .AsQueryable();
    }
}
