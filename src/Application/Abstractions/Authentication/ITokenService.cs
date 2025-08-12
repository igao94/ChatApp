using Domain.Entites;

namespace Application.Abstractions.Authentication;

public interface ITokenService
{
    string GetToken(AppUser user, IEnumerable<string> roles);
}
