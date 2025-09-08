using Application.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

internal sealed class LastSeenUserFilter(IUserLastSeenService userLastSeenService) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();

        if (resultContext.HttpContext.User.Identity?.IsAuthenticated == true)
        {
            await userLastSeenService.UpdateLastSeenAsync();
        }
    }
}
