using Application.Messages.DTOs;
using MediatR;
using Shared;
using Shared.Pagination;

namespace Application.Messages.Queries.SearchChat;

public sealed record SearchChatQuery(SearchChatParams MessageParams)
    : IRequest<Result<CursorPagination<MessageDto, DateTime?>>>;
