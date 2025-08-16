using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.UpdateUser;
using Application.Users.DTOs;
using Application.Users.Queries.GetUserById;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public sealed class UsersController : BaseApiController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUserById(Guid id)
    {
        return HandleResult(await Mediator.Send(new GetUserByIdQuery(id)));
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(UpdateUserCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteUser()
    {
        return HandleResult(await Mediator.Send(new DeleteUserCommand()));
    }
}
