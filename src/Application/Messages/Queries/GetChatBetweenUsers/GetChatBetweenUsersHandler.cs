using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using Application.Messages.DTOs;
using AutoMapper;
using MediatR;
using Shared;
using Shared.Pagination;

namespace Application.Messages.Queries.GetChatBetweenUsers;

internal sealed class GetChatBetweenUsersHandler(IUnitOfWork unitOfWork,
    IUserContext userContext,
    IMapper mapper)
    : IRequestHandler<GetChatBetweenUsersQuery, Result<CursorPagination<MessageDto, DateTime?>>>
{
    public async Task<Result<CursorPagination<MessageDto, DateTime?>>> Handle(GetChatBetweenUsersQuery request,
        CancellationToken cancellationToken)
    {
        var (messages, nextCursor) = await unitOfWork.MessageRepository.GetChatAsync(userContext.UserId,
            request.ChatParams.RecipientId,
            request.ChatParams.Cursor,
            request.ChatParams.PageSize);

        return Result<CursorPagination<MessageDto, DateTime?>>
            .Success(new CursorPagination<MessageDto, DateTime?>
            {
                Items = mapper.Map<IReadOnlyList<MessageDto>>(messages),
                NextCursor = nextCursor
            });
    }
}
