using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using MediatR;
using Shared;

namespace Application.Accounts.Commands;

internal sealed class LoginHandler(IUnitOfWork unitOfWork,
    ITokenService tokenService) : IRequestHandler<LoginCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetAsync(u => u.Email == request.Email);

        if (user is null)
        {
            return Result<string>.Failure("User not found.");
        }

        var token = tokenService.GetToken(user);

        return Result<string>.Success(token);
    }
}
