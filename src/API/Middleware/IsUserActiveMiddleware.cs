using Application.Abstractions;

namespace API.Middleware;

internal sealed class IsUserActiveMiddleware(IUserActiveService userActiveService) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var isActive = await userActiveService.IsUserActiveAsync();

            if (!isActive)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync("User account is deactivated.");

                return;
            }
        }

        await next(context);
    }
}
