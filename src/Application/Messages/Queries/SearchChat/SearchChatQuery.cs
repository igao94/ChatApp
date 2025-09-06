using Application.Messages.DTOs;
using MediatR;
using Shared;

namespace Application.Messages.Queries.SearchChat;

public sealed record SearchChatQuery(Guid RecipientId, string? SearchTerm)
    : IRequest<Result<IReadOnlyList<MessageDto>>>;
