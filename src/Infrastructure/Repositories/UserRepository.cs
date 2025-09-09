using Application.Abstractions.Repositories;
using Domain.Entites;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class UserRepository(AppDbContext context)
    : RepositoryBase<AppUser>(context), IUserRepository
{
    public async Task<(IReadOnlyList<AppUser>, DateTime?)> GetAllUsersForAdminAsync(string? searchTerm,
        int pageSize,
        DateTime? cursor)
    {
        var query = _context.Users
            .IgnoreQueryFilters()
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(u => u.Name.Contains(searchTerm));
        }

        query = query.OrderByDescending(u => u.CreatedAt);

        return await PaginateByCursorDescAsync(query, pageSize, cursor);
    }
}
