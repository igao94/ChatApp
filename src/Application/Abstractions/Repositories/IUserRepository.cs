using Domain.Entites;

namespace Application.Abstractions.Repositories;

public interface IUserRepository : IRepositoryBase<AppUser>
{
    Task<(IReadOnlyList<AppUser>, DateTime?)> GetAllUsersForAdminAsync(string? searchTerm,
        int pageSize,
        DateTime? cursor);
    Task<(IReadOnlyList<AppUser>, DateTime?)> SearchUsersAsync(string searchTerm,
        int pageSize,
        DateTime? cursor);
}
