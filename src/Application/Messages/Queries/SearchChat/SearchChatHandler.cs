using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using Application.Messages.DTOs;
using AutoMapper;
using MediatR;
using Shared;

namespace Application.Messages.Queries.SearchChat;

internal sealed class SearchChatHandler(IUnitOfWork unitOfWork,
    IUserContext userContext,
    IMapper mapper) : IRequestHandler<SearchChatQuery, Result<IReadOnlyList<MessageDto>>>
{
    public async Task<Result<IReadOnlyList<MessageDto>>> Handle(SearchChatQuery request,
        CancellationToken cancellationToken)
    {
        var messages = await unitOfWork.MessageRepository
            .SearchChatAsync(userContext.UserId, request.RecipientId, request.SearchTerm);

        return Result<IReadOnlyList<MessageDto>>.Success(mapper.Map<IReadOnlyList<MessageDto>>(messages));
    }
}
