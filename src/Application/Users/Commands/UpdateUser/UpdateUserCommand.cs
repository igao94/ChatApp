using MediatR;
using Shared;

namespace Application.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(Guid Id, string? About) : IRequest<Result<Unit>>;

