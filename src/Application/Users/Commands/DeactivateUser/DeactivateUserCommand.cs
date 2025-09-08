using MediatR;
using Shared;

namespace Application.Users.Commands.DeactivateUser;

public sealed record DeactivateUserCommand : IRequest<Result<Unit>>;
