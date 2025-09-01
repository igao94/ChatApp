namespace Application.Abstractions.Repositories;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IRoleRepository RoleRepository { get; }
    IUserRoleRepository UserRoleRepository { get; }
    IMessageRepository MessageRepository { get; }
    Task<bool> SaveChangesAsync();
}
