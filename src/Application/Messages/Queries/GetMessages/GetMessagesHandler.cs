using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using Application.Helpers;
using Application.Messages.DTOs;
using AutoMapper;
using MediatR;
using Shared;

namespace Application.Messages.Queries.GetMessages;

internal sealed class GetMessagesHandler(IUnitOfWork unitOfWork,
    IUserContext userContext,
    IMapper mapper) : IRequestHandler<GetMessagesQuery, Result<IReadOnlyList<MessageDto>>>
{
    public async Task<Result<IReadOnlyList<MessageDto>>> Handle(GetMessagesQuery request,
        CancellationToken cancellationToken)
    {
        var userResult = await unitOfWork.GetUserByIdAsync(userContext.UserId);

        if (userResult.IsFailure)
        {
            return Result<IReadOnlyList<MessageDto>>.Failure(userResult.Error!);
        }

        var user = userResult.Value!;

        var messages = await unitOfWork.MessageRepository
            .GetMessagesForUserAsync(user.Id, request.Container);

        return Result<IReadOnlyList<MessageDto>>.Success(mapper.Map<IReadOnlyList<MessageDto>>(messages));
    }
}
