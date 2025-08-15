using Application.Abstractions.Repositories;
using Application.Users.DTOs;
using AutoMapper;
using MediatR;
using Shared;

namespace Application.Users.Queries.GetById;

internal sealed class GetByIdHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetByIdQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(request.Id);

        if (user is null)
        {
            return Result<UserDto>.Failure("User not found.");
        }

        return Result<UserDto>.Success(mapper.Map<UserDto>(user));
    }
}
