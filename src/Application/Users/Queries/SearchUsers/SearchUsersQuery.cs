using Application.Users.DTOs;
using MediatR;
using Shared;
using Shared.Pagination;

namespace Application.Users.Queries.SearchUsers;

public sealed record SearchUsersQuery(UserParams UserParams) 
    : IRequest<Result<CursorPagination<UserDto, DateTime?>>>;
