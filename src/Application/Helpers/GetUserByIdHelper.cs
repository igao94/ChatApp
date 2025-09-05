using Application.Abstractions.Repositories;
using Domain.Entites;
using Shared;

namespace Application.Helpers;

public static class GetUserByIdHelper
{
    public static async Task<Result<AppUser>> GetUserByIdAsync(this IUnitOfWork unitOfWork, Guid id)
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(id);

        return user is not null
            ? Result<AppUser>.Success(user)
            : Result<AppUser>.Failure("User not found.");
    }
}
