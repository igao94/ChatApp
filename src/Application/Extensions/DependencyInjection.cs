using Application.Accounts.Commands;
using Application.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(LoginCommand).Assembly);
        });

        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        return services;
    }
}
