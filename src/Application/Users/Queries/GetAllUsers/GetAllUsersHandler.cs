using Application.Abstractions.Repositories;
using Application.Users.DTOs;
using AutoMapper;
using MediatR;
using Shared;

namespace Application.Users.Queries.GetAllUsers;

internal sealed class GetAllUsersHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllUsersQuery, Result<IReadOnlyList<AdminUserDto>>>
{
    public async Task<Result<IReadOnlyList<AdminUserDto>>> Handle(GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var users = await unitOfWork.UserRepository.GetAllWithIgnoreQueryFilterAsync();

        return Result<IReadOnlyList<AdminUserDto>>.Success(mapper.Map<IReadOnlyList<AdminUserDto>>(users));
    }
}
