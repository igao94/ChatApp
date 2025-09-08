using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using Application.Helpers;
using MediatR;
using Shared;

namespace Application.Users.Commands.DeactivateUser;

internal sealed class DeactivateUserHandler(IUnitOfWork unitOfWork,
    IUserContext userContext) : IRequestHandler<DeactivateUserCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
    {
        var userResult = await unitOfWork.GetUserByIdAsync(userContext.UserId);

        if (userResult.IsFailure)
        {
            return Result<Unit>.Failure(userResult.Error!);
        }

        var user = userResult.Value!;

        user.IsActive = false;

        return await unitOfWork.SaveChangesAsync()
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to deactiavate user.");
    }
}
