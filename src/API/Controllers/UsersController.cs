using Application.Users.Commands.UpdateUser;
using Application.Users.DTOs;
using Application.Users.Queries.GetUserById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public sealed class UsersController : BaseApiController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUserById(Guid id)
    {
        return HandleResult(await Mediator.Send(new GetUserByIdQuery(id)));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(Guid id, string? about)
    {
        return HandleResult(await Mediator.Send(new UpdateUserCommand(id, about)));
    }
}
