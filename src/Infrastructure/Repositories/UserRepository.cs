using Application.Abstractions.Repositories;
using Domain.Entites;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class UserRepository(AppDbContext context) 
    : RepositoryBase<AppUser>(context), IUserRepository
{
    public async Task<IEnumerable<string>> GetUserRolesAsync(Guid id)
    {
        return await _context.AppUserRoles
            .Where(ur => ur.UserId == id)
            .Select(u => u.Role.Name)
            .ToListAsync();
    }
}
