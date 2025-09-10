using Application.Abstractions.Repositories;
using Application.Extensions.Mappings;
using Application.Users.DTOs;
using MediatR;
using Shared;
using Shared.Pagination;

namespace Application.Users.Queries.SearchUsers;

internal sealed class SearchUsersHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<SearchUsersQuery, Result<CursorPagination<UserDto, DateTime?>>>
{
    public async Task<Result<CursorPagination<UserDto, DateTime?>>> Handle(SearchUsersQuery request,
        CancellationToken cancellationToken)
    {
        var (users, nextCursor) = await unitOfWork.UserRepository
            .SearchUsersAsync(request.UserParams.SearchTerm!,
                request.UserParams.PageSize,
                request.UserParams.Cursor);

        return Result<CursorPagination<UserDto, DateTime?>>
            .Success(new CursorPagination<UserDto, DateTime?>
            {
                Items = users.Select(u => u.ToDto()).ToList(),
                NextCursor = nextCursor
            });
    }
}
