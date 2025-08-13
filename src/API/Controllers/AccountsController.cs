using Application.Accounts.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public sealed class AccountsController : BaseApiController
{
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }
}
