using Application.Messages.Commands.SendMessage;
using Application.Messages.DTOs;
using Application.Messages.Queries.GetChatBetweenUsers;
using Application.Messages.Queries.GetMessages;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public sealed class MessagesController : BaseApiController
{
    [HttpPost("send-message")]
    public async Task<ActionResult<MessageDto>> SendMessage(SendMessageCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [HttpGet("get-messages")]
    public async Task<ActionResult<IReadOnlyList<MessageDto>>> GetMessages(string container)
    {
        return HandleResult(await Mediator.Send(new GetMessagesQuery(container)));
    }

    [HttpGet("get-chat/{recipientId}")]
    public async Task<ActionResult<IReadOnlyList<MessageDto>>> GetChat(Guid recipientId)
    {
        return HandleResult(await Mediator.Send(new GetChatBetweenUsersQuery(recipientId)));
    }
}
