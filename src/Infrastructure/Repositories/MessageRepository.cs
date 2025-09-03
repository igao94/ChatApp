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

    public async Task<IReadOnlyList<Message>> GetChatAsync(Guid currentUserId, Guid recipientId)
    {
        // In production, I could use ExecuteUpdateAsync to mark the message as read directly in the database.
        // It's commented out because ExecuteUpdateAsync does not work with the in-memory database, used for testing.

        // await _context.Messages
        //    .Where(m => m.RecipientId == currentUserId
        //        && m.SenderId == recipientId
        //        && m.DateRead == null)
        //    .ExecuteUpdateAsync(setters => setters.SetProperty(m => m.DateRead, DateTime.UtcNow));

        return await _context.Messages
            .AsNoTracking()
            .Include(m => m.Sender)
            .Include(m => m.Recipient)
            .Where(m => (m.RecipientId == currentUserId && m.SenderId == recipientId && !m.RecipientDeleted)
                || (m.SenderId == currentUserId && m.RecipientId == recipientId && !m.SenderDeleted))
            .OrderBy(m => m.CreatedAt)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Message>> GetMessagesForUserAsync(Guid userId, string container)
    {
        var query = _context.Messages
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
}
