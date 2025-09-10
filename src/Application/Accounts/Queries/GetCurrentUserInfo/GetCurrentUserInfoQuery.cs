using Application.Accounts.DTOs;
using MediatR;
using Shared;

namespace Application.Accounts.Queries.GetCurrentUserInfo;

public sealed record GetCurrentUserInfoQuery : IRequest<Result<CurrentUserDto>>;
