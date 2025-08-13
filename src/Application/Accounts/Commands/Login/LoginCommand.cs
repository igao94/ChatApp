using MediatR;
using Shared;

namespace Application.Accounts.Commands.Login;

public sealed record LoginCommand(string Email, string Password) : IRequest<Result<string>>;
