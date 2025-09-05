using Application.Abstractions.Repositories;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

internal sealed class UnitOfWork(IUserRepository userRepository,
    IRoleRepository roleRepository,
    IUserRoleRepository userRoleRepository,
    IMessageRepository messageRepository,
    AppDbContext context) : IUnitOfWork
{
    public IUserRepository UserRepository => userRepository;

    public IRoleRepository RoleRepository => roleRepository;

    public IUserRoleRepository UserRoleRepository => userRoleRepository;

    public IMessageRepository MessageRepository => messageRepository;

    public async Task<bool> SaveChangesAsync() => await context.SaveChangesAsync() > 0;
}
