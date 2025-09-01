using Application.Abstractions.Repositories;
using Domain.Entites;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

internal sealed class MessageRepository(AppDbContext context) 
    : RepositoryBase<Message>(context), IMessageRepository
{

}
