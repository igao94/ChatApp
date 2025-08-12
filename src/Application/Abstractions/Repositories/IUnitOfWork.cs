namespace Application.Abstractions.Repositories;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    Task<bool> SaveChangesAsync();
}
