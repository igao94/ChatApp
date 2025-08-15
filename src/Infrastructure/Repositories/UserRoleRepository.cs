using Application.Abstractions.Repositories;
using Domain.Entites;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class UserRoleRepository(AppDbContext context) 
    : RepositoryBase<AppUserRole>(context), IUserRoleRepository
{
    public async Task<IEnumerable<string>> GetUserRoleNamesAsync(Guid id)
    {
        return await _context.AppUserRoles
            .Where(ur => ur.UserId == id)
            .Select(u => u.Role.Name)
            .ToListAsync();
    }

    public void AddUserToRole(Guid userId, Guid roleId)
    {
        _context.AppUserRoles.Add(new AppUserRole
        {
            UserId = userId,
            RoleId = roleId
        });
    }
}
