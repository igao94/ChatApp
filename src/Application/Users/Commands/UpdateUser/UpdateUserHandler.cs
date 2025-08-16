using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using MediatR;
using Shared;

namespace Application.Users.Commands.UpdateUser;

internal sealed class UpdateUserHandler(IUnitOfWork unitOfWork,
    IUserContext userContext): IRequestHandler<UpdateUserCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(userContext.UserId);

        if (user is null)
        {
            return Result<Unit>.Failure("User not found.");
        }

        user.About = request.About;

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to update user.");
    }
}
