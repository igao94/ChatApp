using Application.Accounts.DTOs;
using MediatR;
using Shared;

namespace Application.Accounts.Commands.Register;

public sealed record RegisterCommand(string Name, string Email, string Password, string? About = null)
    : IRequest<Result<AccountDto>>;
