using Application.Users.Commands.DeactivateUser;
using Application.Users.Commands.UpdateUser;
using Application.Users.DTOs;
using Application.Users.Queries.GetAllUsers;
using Application.Users.Queries.GetUserById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public sealed class UsersController : BaseApiController
{
    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<UserDto>>> GetAllUsers()
    {
        return HandleResult(await Mediator.Send(new GetAllUsersQuery()));
    }

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

    [HttpPut("deactivate-user")]
    public async Task<ActionResult> DeactivateUser()
    {
        return HandleResult(await Mediator.Send(new DeactivateUserCommand()));
    }
}
