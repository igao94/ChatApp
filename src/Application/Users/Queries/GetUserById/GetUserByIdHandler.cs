using Application.Abstractions.Repositories;
using Application.Users.DTOs;
using AutoMapper;
using MediatR;
using Shared;

namespace Application.Users.Queries.GetUserById;

internal sealed class GetUserByIdHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(request.Id);

        if (user is null)
        {
            return Result<UserDto>.Failure("User not found.");
        }

        return Result<UserDto>.Success(mapper.Map<UserDto>(user));
    }
}
