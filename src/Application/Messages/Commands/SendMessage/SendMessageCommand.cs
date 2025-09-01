using Application.Messages.DTOs;
using MediatR;
using Shared;

namespace Application.Messages.Commands.SendMessage;

public sealed record SendMessageCommand(Guid RecipientId, string Content) : IRequest<Result<MessageDto>>;
