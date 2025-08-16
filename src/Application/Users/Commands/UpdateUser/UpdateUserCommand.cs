using MediatR;
using Shared;

namespace Application.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(string? About) : IRequest<Result<Unit>>;

