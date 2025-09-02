using Application.Users.DTOs;
using MediatR;
using Shared;

namespace Application.Users.Queries.GetAllUsers;

public sealed record GetAllUsersQuery : IRequest<Result<IReadOnlyList<UserDto>>>;
