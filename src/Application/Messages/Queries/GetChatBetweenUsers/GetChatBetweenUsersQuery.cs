using Application.Messages.DTOs;
using MediatR;
using Shared;
using Shared.Pagination;

namespace Application.Messages.Queries.GetChatBetweenUsers;

public sealed record GetChatBetweenUsersQuery(ChatParams ChatParams) 
    : IRequest<Result<CursorPagination<MessageDto, DateTime?>>>;
