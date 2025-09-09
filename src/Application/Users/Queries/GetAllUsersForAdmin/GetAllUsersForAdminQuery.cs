using Application.Users.DTOs;
using MediatR;
using Shared;
using Shared.Pagination;

namespace Application.Users.Queries.GetAllUsersForAdmin;

public sealed record GetAllUsersForAdminQuery(UserParams UserParams) 
    : IRequest<Result<CursorPagination<AdminUserDto, DateTime?>>>;
