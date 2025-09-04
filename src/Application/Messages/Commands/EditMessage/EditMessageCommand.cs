using MediatR;
using Shared;

namespace Application.Messages.Commands.EditMessage;

public sealed record EditMessageCommand(Guid Id, string Content) : IRequest<Result<Unit>>;
