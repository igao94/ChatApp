using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using Application.Helpers;
using Application.Messages.DTOs;
using AutoMapper;
using MediatR;
using Shared;
using Shared.Pagination;

namespace Application.Messages.Queries.GetMessages;

internal sealed class GetMessagesHandler(IUnitOfWork unitOfWork,
    IUserContext userContext,
    IMapper mapper) : IRequestHandler<GetMessagesQuery, Result<CursorPagination<MessageDto, DateTime?>>>
{
    public async Task<Result<CursorPagination<MessageDto, DateTime?>>> Handle(GetMessagesQuery request,
        CancellationToken cancellationToken)
    {
        var userResult = await unitOfWork.GetUserByIdAsync(userContext.UserId);

        if (userResult.IsFailure)
        {
            return Result<CursorPagination<MessageDto, DateTime?>>.Failure(userResult.Error!);
        }

        var user = userResult.Value!;

        var (messages, nextCursor) = await unitOfWork.MessageRepository
            .GetMessagesForUserAsync(user.Id,
                request.MessageParams.Container,
                request.MessageParams.PageSize,
                request.MessageParams.Cursor);

        return Result<CursorPagination<MessageDto, DateTime?>>
            .Success(new CursorPagination<MessageDto, DateTime?>
            {
                Items = mapper.Map<IReadOnlyList<MessageDto>>(messages),
                NextCursor = nextCursor
            });
    }
}
