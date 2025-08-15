using Application.Users.DTOs;
using Application.Users.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public sealed class UsersController : BaseApiController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUserById(Guid id)
    {
        return HandleResult(await Mediator.Send(new GetByIdQuery(id)));
    }
}
