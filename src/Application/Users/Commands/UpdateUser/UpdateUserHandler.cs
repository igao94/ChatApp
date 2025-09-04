using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using Application.Helpers;
using MediatR;
using Shared;

namespace Application.Users.Commands.UpdateUser;

internal sealed class UpdateUserHandler(IUnitOfWork unitOfWork,
    IUserContext userContext) : IRequestHandler<UpdateUserCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.GetUserByIdAsync(userContext.UserId);

        if (result.IsFailure)
        {
            return Result<Unit>.Failure(result.Error!);
        }

        var user = result.Value!;

        user.About = request.About;

        return await unitOfWork.SaveChangesAsync()
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to update user.");
    }
}
