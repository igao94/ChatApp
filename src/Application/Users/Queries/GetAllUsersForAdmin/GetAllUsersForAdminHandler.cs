using Application.Abstractions.Repositories;
using Application.Users.DTOs;
using AutoMapper;
using MediatR;
using Shared;
using Shared.Pagination;

namespace Application.Users.Queries.GetAllUsersForAdmin;

internal sealed class GetAllUsersForAdminHandler(IUnitOfWork unitOfWork,
    IMapper mapper) 
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
                Items = mapper.Map<IReadOnlyList<AdminUserDto>>(users),
                NextCursor = nextCursor
            });
    }
}
