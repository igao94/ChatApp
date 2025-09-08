using Application.Abstractions;
using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;

namespace Application.Services.UserActivity;

internal sealed class UserActiveService(IUnitOfWork unitOfWork,
    IUserContext userContext) : IUserActiveService
{
    public async Task<bool> IsUserActiveAsync()
    {
        var user = await unitOfWork.UserRepository
            .GetWithIgnoreQueryFilterAsync(u => u.Id == userContext.UserId);

        if (user is null)
        {
            return false;
        }

        return user.IsActive;
    }
}
