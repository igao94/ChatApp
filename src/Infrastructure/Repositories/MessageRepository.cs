using Application.Abstractions.Repositories;
using Domain.Entites;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class MessageRepository(AppDbContext context)
    : RepositoryBase<Message>(context), IMessageRepository
{
    public async Task<IReadOnlyList<Message>> GetMessagesForUser(Guid userId, string container)
    {
        var query = _context.Messages
            .Include(m => m.Recipient)
            .Include(m => m.Sender)
            .OrderByDescending(m => m.CreatedAt)
            .AsQueryable();

        query = container switch
        {
            "Inbox" => query.Where(m => m.RecipientId == userId),
            "Outbox" => query.Where(m => m.SenderId == userId),
            _ => query.Where(m => m.RecipientId == userId)
        };

        return await query.ToListAsync();
    }
}
