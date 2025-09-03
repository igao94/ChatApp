using MediatR;
using Shared;

namespace Application.Messages.Commands.DeleteMessage;

public sealed record DeleteMessageCommand(Guid Id) : IRequest<Result<Unit>>;

