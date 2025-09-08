namespace Application.Abstractions;

public interface IUserLastSeenService
{
    Task UpdateLastSeenAsync();
}
