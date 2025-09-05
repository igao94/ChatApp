using Application.Abstractions.Repositories;
using Application.Helpers;
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
        var userResult = await unitOfWork.GetUserByIdAsync(request.Id);

        return userResult.IsFailure
            ? Result<UserDto>.Failure(userResult.Error!)
            : Result<UserDto>.Success(mapper.Map<UserDto>(userResult.Value));
    }
}
