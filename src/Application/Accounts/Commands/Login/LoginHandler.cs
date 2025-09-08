using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using Domain.Entites;
using MediatR;
using Shared;

namespace Application.Accounts.Commands.Login;

internal sealed class LoginHandler(IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher,
    ITokenService tokenService) : IRequestHandler<LoginCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository
            .GetWithIgnoreQueryFilterAsync(u => u.Email == request.Email);

        if (user is null)
        {
            return Result<string>.Failure("Invalid email or password.");
        }

        if (!IsPasswordVerified(request.Password, user.PasswordHash))
        {
            return Result<string>.Failure("Invalid email or password.");
        }

        if (!user.IsActive)
        {
            await ActivateUserAsync(user);
        }

        var roles = await unitOfWork.UserRoleRepository.GetUserRoleNamesAsync(user.Id);

        var token = tokenService.GetToken(user, roles);

        return Result<string>.Success(token);
    }

    private bool IsPasswordVerified(string password, string passwordHash)
    {
        return passwordHasher.Verify(password, passwordHash);
    }

    private async Task ActivateUserAsync(AppUser user)
    {
        user.IsActive = true;

        await unitOfWork.SaveChangesAsync();
    }
}
