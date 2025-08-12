using Application.Abstractions.Repositories;
using Domain.Entites;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

internal sealed class UserRepository(AppDbContext context) 
    : RepositoryBase<AppUser>(context), IUserRepository
{

}
