using Application.Abstractions.Authentication;
using Application.Abstractions.Repositories;
using Application.Messages.DTOs;
using AutoMapper;
using Domain.Entites;
using MediatR;
using Shared;

namespace Application.Messages.Commands.SendMessage;

internal sealed class SendMessageHandler(IUnitOfWork unitOfWork,
    IUserContext userContext,
    IMapper mapper) : IRequestHandler<SendMessageCommand, Result<MessageDto>>
{
    public async Task<Result<MessageDto>> Handle(SendMessageCommand request,
        CancellationToken cancellationToken)
    {
        var sender = await unitOfWork.UserRepository.GetByIdAsync(userContext.UserId);

        var recipient = await unitOfWork.UserRepository.GetByIdAsync(request.RecipientId);

        if (recipient is null || sender is null || sender.Id == recipient.Id)
        {
            return Result<MessageDto>.Failure("Can't send this message.");
        }

        var message = new Message
        {
            SenderId = sender.Id,
            RecipientId = recipient.Id,
            Content = request.Content,
        };

        unitOfWork.MessageRepository.Add(message);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<MessageDto>.Success(mapper.Map<MessageDto>(message))
            : Result<MessageDto>.Failure("Failed to send message.");
    }
}
