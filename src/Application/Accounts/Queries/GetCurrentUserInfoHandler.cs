using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using Application.Accounts.DTOs;
using Application.Helpers;
using MediatR;
using Shared;

namespace Application.Accounts.Queries;

internal sealed class GetCurrentUserInfoHandler(IUnitOfWork unitOfWork,
    IUserContext userContext) : IRequestHandler<GetCurrentUserInfoQuery, Result<CurrentUserDto>>
{
    public async Task<Result<CurrentUserDto>> Handle(GetCurrentUserInfoQuery request,
        CancellationToken cancellationToken)
    {
        var userResult = await unitOfWork.GetUserByIdAsync(userContext.UserId);

        if (userResult.IsFailure)
        {
            return Result<CurrentUserDto>.Failure(userResult.Error!);
        }

        var user = userResult.Value!;

        return Result<CurrentUserDto>.Success(new CurrentUserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        });
    }
}
