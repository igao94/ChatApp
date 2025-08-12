using Application.Accounts.Commands;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public sealed class AccountsController : BaseApiController
{
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }
}
