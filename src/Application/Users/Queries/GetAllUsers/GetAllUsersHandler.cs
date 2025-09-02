using Application.Abstractions.Repositories;
using Application.Users.DTOs;
using AutoMapper;
using MediatR;
using Shared;

namespace Application.Users.Queries.GetAllUsers;

internal sealed class GetAllUsersHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllUsersQuery, Result<IReadOnlyList<UserDto>>>
{
    public async Task<Result<IReadOnlyList<UserDto>>> Handle(GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var users = await unitOfWork.UserRepository.GetAllAsync();

        return Result<IReadOnlyList<UserDto>>.Success(mapper.Map<IReadOnlyList<UserDto>>(users));
    }
}
