namespace Application.Abstractions.Repositories;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IRoleRepository RoleRepository { get; }
    Task<bool> SaveChangesAsync();
}
