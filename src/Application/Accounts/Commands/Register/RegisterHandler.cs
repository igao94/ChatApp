using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using Application.Accounts.DTOs;
using Domain.Entites;
using MediatR;
using Shared;

namespace Application.Accounts.Commands.Register;

internal sealed class RegisterHandler(IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher) : IRequestHandler<RegisterCommand, Result<AccountDto>>
{
    public async Task<Result<AccountDto>> Handle(RegisterCommand request,
        CancellationToken cancellationToken)
    {
        if (await unitOfWork.UserRepository.AnyAsync(u => u.Email == request.Email))
        {
            return Result<AccountDto>.Failure("Email is already taken.");
        }

        var passwordHash = passwordHasher.Hash(request.Password);

        var user = new AppUser
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = passwordHash,
            About = request.About
        };

        unitOfWork.UserRepository.Add(user);

        await AddUserToRoleAsync(user.Id);

        return await unitOfWork.SaveChangesAsync()
            ? Result<AccountDto>.Success(new AccountDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                About = user.About
            })
            : Result<AccountDto>.Failure("Failed to create user.");
    }

    private async Task AddUserToRoleAsync(Guid userId)
    {
        var role = await unitOfWork.RoleRepository.GetAsync(r => r.Name == "User");

        unitOfWork.UserRoleRepository.AddUserToRole(userId, role!.Id);
    }
}
