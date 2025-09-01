using Application.Messages.Commands.SendMessage;
using Application.Messages.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public sealed class MessagesController : BaseApiController
{
    [HttpPost("send-message")]
    public async Task<ActionResult<MessageDto>> SendMessage(SendMessageCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }
}
