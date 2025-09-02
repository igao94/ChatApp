using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using Application.Messages.DTOs;
using AutoMapper;
using MediatR;
using Shared;

namespace Application.Messages.Queries.GetChatBetweenUsers;

internal sealed class GetChatBetweenUsersHandler(IUnitOfWork unitOfWork,
    IUserContext userContext,
    IMapper mapper) : IRequestHandler<GetChatBetweenUsersQuery, Result<IReadOnlyList<MessageDto>>>
{
    public async Task<Result<IReadOnlyList<MessageDto>>> Handle(GetChatBetweenUsersQuery request,
        CancellationToken cancellationToken)
    {
        var messages = await unitOfWork.MessageRepository
            .GetChatAsync(userContext.UserId, request.RecipientId);

        return Result<IReadOnlyList<MessageDto>>.Success(mapper.Map<IReadOnlyList<MessageDto>>(messages));
    }
}
