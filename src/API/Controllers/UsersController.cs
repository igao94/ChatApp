using Application.Users;
using Application.Users.Commands.DeactivateUser;
using Application.Users.Commands.UpdateUser;
using Application.Users.DTOs;
using Application.Users.Queries.GetAllUsersForAdmin;
using Application.Users.Queries.GetUserById;
using Application.Users.Queries.SearchUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Pagination;

namespace API.Controllers;

public sealed class UsersController : BaseApiController
{
    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet]
    public async Task<ActionResult<CursorPagination<AdminUserDto, DateTime?>>> GetAllUsersForAdmin(
        [FromQuery] UserParams userParams)
    {
        return HandleResult(await Mediator.Send(new GetAllUsersForAdminQuery(userParams)));
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

    [HttpGet("search-users")]
    public async Task<ActionResult<CursorPagination<UserDto, DateTime?>>> SearchUsers(
        [FromQuery] UserParams userParams)
    {
        return HandleResult(await Mediator.Send(new SearchUsersQuery(userParams)));
    }
}
