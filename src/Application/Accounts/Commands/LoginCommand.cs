using MediatR;
using Shared;

namespace Application.Accounts.Commands;

public sealed class LoginCommand(string email, string password) : IRequest<Result<string>>
{
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
}
