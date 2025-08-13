using Application.Accounts.Commands.Login;
using Application.Accounts.Commands.Register;
using Application.Accounts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public sealed class AccountsController : BaseApiController
{
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [HttpPost("Register")]
    public async Task<ActionResult<AccountDto>> Register(RegisterCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }
}
