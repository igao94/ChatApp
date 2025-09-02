using Domain.Entites;

namespace Application.Abstractions.Repositories;

public interface IUserRoleRepository : IRepositoryBase<AppUserRole>
{
    Task<IReadOnlyList<string>> GetUserRoleNamesAsync(Guid id);
    void AddUserToRole(Guid userId, Guid roleId);
}
