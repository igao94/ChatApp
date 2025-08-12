using Application.Abstractions.Repositories;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

internal sealed class UnitOfWork(IUserRepository userRepository,
    AppDbContext context) : IUnitOfWork
{
    public IUserRepository UserRepository => userRepository;

    public async Task<bool> SaveChangesAsync() => await context.SaveChangesAsync() > 0;
}
