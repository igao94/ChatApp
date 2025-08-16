using MediatR;
using Shared;

namespace Application.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand : IRequest<Result<Unit>>;
