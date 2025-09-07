namespace Application.Abstractions;

public interface IUserActivityService
{
    Task UpdateLastSeenAsync();
}
