using API.Middleware;

namespace API.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddTransient<ExceptionMiddleware>();

        return services;
    }
}
