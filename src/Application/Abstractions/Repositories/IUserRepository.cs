using Domain.Entites;

namespace Application.Abstractions.Repositories;

public interface IUserRepository : IRepositoryBase<AppUser>
{
    Task<IEnumerable<string>> GetUserRolesAsync(Guid id);
}
