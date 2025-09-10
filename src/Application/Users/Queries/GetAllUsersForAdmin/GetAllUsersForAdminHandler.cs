using Application.Abstractions.Repositories;
using Application.Extensions.Mappings;
using Application.Users.DTOs;
using MediatR;
using Shared;
using Shared.Pagination;

namespace Application.Users.Queries.GetAllUsersForAdmin;

internal sealed class GetAllUsersForAdminHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllUsersForAdminQuery, Result<CursorPagination<AdminUserDto, DateTime?>>>
{
    public async Task<Result<CursorPagination<AdminUserDto, DateTime?>>> Handle(
        GetAllUsersForAdminQuery request,
        CancellationToken cancellationToken)
    {
        var (users, nextCursor) = await unitOfWork.UserRepository
            .GetAllUsersForAdminAsync(request.UserParams.SearchTerm,
                request.UserParams.PageSize,
                request.UserParams.Cursor);

        return Result<CursorPagination<AdminUserDto, DateTime?>>
            .Success(new CursorPagination<AdminUserDto, DateTime?>
            {
                Items = users.Select(u => u.ToAdminDto()).ToList(),
                NextCursor = nextCursor
            });
    }
}
