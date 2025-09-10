using Application.Abstractions.Repositories;
using Application.Extensions.Mappings;
using Application.Helpers;
using Application.Users.DTOs;
using MediatR;
using Shared;

namespace Application.Users.Queries.GetUserById;

internal sealed class GetUserByIdHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var userResult = await unitOfWork.GetUserByIdAsync(request.Id);

        return userResult.IsFailure
            ? Result<UserDto>.Failure(userResult.Error!)
            : Result<UserDto>.Success(userResult.Value!.ToDto());
    }
}
