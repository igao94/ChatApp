using Application.Abstractions;
using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;

namespace Application.Services;

internal sealed class UserActivityService(IUnitOfWork unitOfWork,
    IUserContext userContext) : IUserActivityService
{
    public async Task UpdateLastSeenAsync()
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(userContext.UserId);

        if (user is null)
        {
            return;
        }

        user.LastSeen = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync();
    }
}
