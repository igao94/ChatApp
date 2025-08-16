using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using Application.Accounts.DTOs;
using MediatR;
using Shared;

namespace Application.Accounts.Queries;

internal sealed class GetCurrentUserInfoHandler(IUnitOfWork unitOfWork,
    IUserContext userContext) : IRequestHandler<GetCurrentUserInfoQuery, Result<CurrentUserDto>>
{
    public async Task<Result<CurrentUserDto>> Handle(GetCurrentUserInfoQuery request,
        CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(userContext.UserId);

        if (user is null)
        {
            return Result<CurrentUserDto>.Failure("Please log in.");
        }

        return Result<CurrentUserDto>.Success(new CurrentUserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        });
    }
}
