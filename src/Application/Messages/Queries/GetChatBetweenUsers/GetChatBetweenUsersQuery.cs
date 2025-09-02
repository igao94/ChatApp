using Application.Messages.DTOs;
using MediatR;
using Shared;

namespace Application.Messages.Queries.GetChatBetweenUsers;

public sealed record GetChatBetweenUsersQuery(Guid RecipientId) : IRequest<Result<IReadOnlyList<MessageDto>>>;
