using API.Filters;
using API.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace API.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers(opt =>
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            opt.Filters.Add(new AuthorizeFilter(policy));

            opt.Filters.Add(typeof(LastSeenUserFilter));
        });

        services.AddScoped<ExceptionMiddleware>();

        services.AddScoped<IsUserActiveMiddleware>();

        return services;
    }
}
