using MediatR;
using Shared;

namespace Application.Messages.Commands.MarkMessagesAsRead;

public sealed record MarkMessagesAsReadCommand(Guid RecipientId) : IRequest<Result<Unit>>;
