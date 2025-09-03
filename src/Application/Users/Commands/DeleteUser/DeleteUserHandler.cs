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

        await RemoveUserFromRolesAsync(user.Id);

        await DetachUserFromMessagesAsync(user.Id);

        unitOfWork.UserRepository.Delete(user);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete user.");
    }

    private async Task RemoveUserFromRolesAsync(Guid id)
    {
        var roles = await unitOfWork.UserRoleRepository.GetAllAsync(ur => ur.UserId == id);

        unitOfWork.UserRoleRepository.DeleteAll(roles);
    }

    private async Task DetachUserFromMessagesAsync(Guid id)
    {
        // In-memory database is used by default to make testing easier.
        // If you switch to SQL Server, you can use the method below and comment out the rest of the code.

        // await unitOfWork.MessageRepository.NullifyUserIdsInMessagesAsync(id);

        var messages = await unitOfWork.MessageRepository
            .GetAllAsync(m => m.SenderId == id || m.RecipientId == id);

        foreach (var message in messages)
        {
            message.SenderId = message.SenderId == id ? null : message.SenderId;

            message.RecipientId = message.RecipientId == id ? null : message.RecipientId;
        }
    }
}
