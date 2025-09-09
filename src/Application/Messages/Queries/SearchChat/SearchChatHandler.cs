using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using Application.Messages.DTOs;
using AutoMapper;
using MediatR;
using Shared;
using Shared.Pagination;

namespace Application.Messages.Queries.SearchChat;

internal sealed class SearchChatHandler(IUnitOfWork unitOfWork,
    IUserContext userContext,
    IMapper mapper) : IRequestHandler<SearchChatQuery, Result<CursorPagination<MessageDto, DateTime?>>>
{
    public async Task<Result<CursorPagination<MessageDto, DateTime?>>> Handle(SearchChatQuery request,
        CancellationToken cancellationToken)
    {
        var (messages, nextCursor) = await unitOfWork.MessageRepository
            .SearchChatAsync(userContext.UserId, request.MessageParams.RecipientId,
                request.MessageParams.PageSize,
                request.MessageParams.Cursor,
                request.MessageParams.SearchTerm!);

        return Result<CursorPagination<MessageDto, DateTime?>>
            .Success(new CursorPagination<MessageDto, DateTime?>
            {
                Items = mapper.Map<IReadOnlyList<MessageDto>>(messages),
                NextCursor = nextCursor
            });
    }
}
