using Application.Messages.DTOs;
using MediatR;
using Shared;

namespace Application.Messages.Queries.GetMessages;

public sealed record GetMessagesQuery(string Container) : IRequest<Result<IReadOnlyList<MessageDto>>>;
