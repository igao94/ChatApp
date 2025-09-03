using MediatR;
using Shared;

namespace Application.Messages.Commands.DeleteMessageForEveryone;

public sealed record DeleteMessageForEveryoneCommand(Guid Id) : IRequest<Result<Unit>>;

