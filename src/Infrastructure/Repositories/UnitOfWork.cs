using Application.Abstractions.Repositories;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

internal sealed class UnitOfWork(IUserRepository userRepository,
    IRoleRepository roleRepository,
    IUserRoleRepository userRoleRepository,
    AppDbContext context) : IUnitOfWork
{
    public IUserRepository UserRepository => userRepository;

    public IRoleRepository RoleRepository => roleRepository;
    public IUserRoleRepository UserRoleRepository => userRoleRepository;

    public async Task<bool> SaveChangesAsync() => await context.SaveChangesAsync() > 0;
}
