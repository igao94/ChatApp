using Application.Messages.DTOs;
using Domain.Entites;

namespace Application.Abstractions.Repositories;

public interface IMessageRepository : IRepositoryBase<Message>
{
    Task<IReadOnlyList<Message>> GetMessagesForUser(Guid userId, string container);
}
