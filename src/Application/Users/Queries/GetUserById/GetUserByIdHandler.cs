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
        var result = await unitOfWork.GetUserByIdAsync(request.Id);

        return result.IsFailure
            ? Result<UserDto>.Failure(result.Error!)
            : Result<UserDto>.Success(mapper.Map<UserDto>(result.Value));
    }
}
