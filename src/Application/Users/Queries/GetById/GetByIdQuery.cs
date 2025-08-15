using Application.Users.DTOs;
using MediatR;
using Shared;

namespace Application.Users.Queries.GetById;

public sealed record GetByIdQuery(Guid Id) : IRequest<Result<UserDto>>;
