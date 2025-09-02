using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using MediatR;
using Shared;

namespace Application.Users.Commands.DeleteUser;

internal sealed class DeleteUserHandler(IUnitOfWork unitOfWork,
    IUserContext userContext) : IRequestHandler<DeleteUserCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(userContext.UserId);

        if (user is null)
        {
            return Result<Unit>.Failure("User not found.");
        }

        var roles = await unitOfWork.UserRoleRepository.GetAllAsync(ur => ur.UserId == user.Id);

        unitOfWork.UserRoleRepository.DeleteAll(roles);

        unitOfWork.UserRepository.Delete(user);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete user.");
    }
}
