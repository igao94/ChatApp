using Application.Users.DTOs;
using MediatR;
using Shared;

namespace Application.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<Result<UserDto>>;
