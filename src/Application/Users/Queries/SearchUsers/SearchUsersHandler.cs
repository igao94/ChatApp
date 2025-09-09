using Application.Abstractions.Repositories;
using Application.Users.DTOs;
using AutoMapper;
using MediatR;
using Shared;
using Shared.Pagination;

namespace Application.Users.Queries.SearchUsers;

internal sealed class SearchUsersHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<SearchUsersQuery, Result<CursorPagination<UserDto, DateTime?>>>
{
    public async Task<Result<CursorPagination<UserDto, DateTime?>>> Handle(SearchUsersQuery request,
        CancellationToken cancellationToken)
    {
        var (users, nextCursor) = await unitOfWork.UserRepository
            .SearchUsersAsync(request.UserParams.SearchTerm,
                request.UserParams.PageSize,
                request.UserParams.Cursor);

        return Result<CursorPagination<UserDto, DateTime?>>
            .Success(new CursorPagination<UserDto, DateTime?>
            {
                Items = mapper.Map<IReadOnlyList<UserDto>>(users),
                NextCursor = nextCursor
            });
    }
}
