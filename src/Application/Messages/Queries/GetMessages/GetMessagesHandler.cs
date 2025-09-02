using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
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
        var user = await unitOfWork.UserRepository.GetByIdAsync(userContext.UserId);

        if (user is null)
        {
            return Result<IReadOnlyList<MessageDto>>.Failure("User not logged in.");
        }

        var messages = await unitOfWork.MessageRepository.GetMessagesForUser(user.Id, request.Container);

        return Result<IReadOnlyList<MessageDto>>.Success(mapper.Map<IReadOnlyList<MessageDto>>(messages));
    }
}
