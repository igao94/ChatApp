using Application.Messages;
using Application.Messages.Commands.DeleteMessage;
using Application.Messages.Commands.DeleteMessageForEveryone;
using Application.Messages.Commands.EditMessage;
using Application.Messages.Commands.MarkMessagesAsRead;
using Application.Messages.Commands.SendMessage;
using Application.Messages.DTOs;
using Application.Messages.Queries.GetChatBetweenUsers;
using Application.Messages.Queries.GetMessages;
using Application.Messages.Queries.SearchChat;
using Microsoft.AspNetCore.Mvc;
using Shared.Pagination;

namespace API.Controllers;

public sealed class MessagesController : BaseApiController
{
    [HttpPost("send-message")]
    public async Task<ActionResult<MessageDto>> SendMessage(SendMessageCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [HttpGet("get-messages")]
    public async Task<ActionResult<CursorPagination<MessageDto, DateTime?>>> GetMessages(
        [FromQuery] MessageParams messageParams)
    {
        return HandleResult(await Mediator.Send(new GetMessagesQuery(messageParams)));
    }

    [HttpPut("mark-messages-read/{recipientId}")]
    public async Task<ActionResult> MarkMessagesAsRead(Guid recipientId)
    {
        // Although the application is developed with SQL Server, 
        // the default setup uses an in-memory database so users can test it more easily. 
        // That's why this endpoint handles marking messages as read. 
        // In production, I would use ExecuteUpdateAsync directly when the user fetches the chat.

        return HandleResult(await Mediator.Send(new MarkMessagesAsReadCommand(recipientId)));
    }

    [HttpGet("get-chat")]
    public async Task<ActionResult<CursorPagination<MessageDto, DateTime?>>>
        GetChat([FromQuery] ChatParams chatParams)
    {
        return HandleResult(await Mediator.Send(new GetChatBetweenUsersQuery(chatParams)));
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

    [HttpPut]
    public async Task<ActionResult> EditMessage(EditMessageCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [HttpGet("search-chat")]
    public async Task<ActionResult<CursorPagination<MessageDto, DateTime?>>> SearchChat(
        [FromQuery] SearchChatParams messageParams)
    {
        return HandleResult(await Mediator.Send(new SearchChatQuery(messageParams)));
    }
}
