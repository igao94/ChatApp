using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using MediatR;
using Shared;

namespace Application.Accounts.Commands.Login;

internal sealed class LoginHandler(IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher,
    ITokenService tokenService) : IRequestHandler<LoginCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetAsync(u => u.Email == request.Email);

        if (user is null)
        {
            return Result<string>.Failure("User not found.");
        }

        var verified = passwordHasher.Verify(request.Password, user.PasswordHash);

        if (!verified)
        {
            return Result<string>.Failure("Invalid email or password.");
        }

        var roles = await unitOfWork.UserRepository.GetUserRolesAsync(user.Id);

        var token = tokenService.GetToken(user, roles);

        return Result<string>.Success(token);
    }
}
