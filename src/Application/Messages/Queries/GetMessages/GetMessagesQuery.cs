using Application.Messages.DTOs;
using MediatR;
using Shared;
using Shared.Pagination;

namespace Application.Messages.Queries.GetMessages;

public sealed record GetMessagesQuery(MessageParams MessageParams) 
    : IRequest<Result<CursorPagination<MessageDto, DateTime?>>>;
