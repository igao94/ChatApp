using Application.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

internal sealed class LastSeenUserFilter(IUserActivityService userActivityService) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();

        if (resultContext.HttpContext.User.Identity?.IsAuthenticated == true)
        {
            await userActivityService.UpdateLastSeenAsync();
        }
    }
}
