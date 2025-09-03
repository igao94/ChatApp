using Application.Messages.Commands.DeleteMessage;
using Application.Messages.Commands.DeleteMessageForEveryone;
using Application.Messages.Commands.MarkMessagesAsRead;
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
        // I could use ExecuteUpdateAsync in production to mark the message as read directly in the database.
        // Since it doesn't work with the in-memory database used for testing,
        // I handle it via the command instead.

        await Mediator.Send(new MarkMessagesAsReadCommand(recipientId));

        return HandleResult(await Mediator.Send(new GetChatBetweenUsersQuery(recipientId)));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMessage(Guid id)
    {
        return HandleResult(await Mediator.Send(new DeleteMessageCommand(id)));
    }

    [HttpDelete("delete-message-for-everyone/{id}")]
    public async Task<ActionResult> DeleteMessageForEveryone(Guid id)
    {
        return HandleResult(await Mediator.Send(new DeleteMessageForEveryoneCommand(id)));
    }
}
