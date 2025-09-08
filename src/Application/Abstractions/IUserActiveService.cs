namespace Application.Abstractions;

public interface IUserActiveService
{
    Task<bool> IsUserActiveAsync();
}
