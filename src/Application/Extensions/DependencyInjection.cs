using Application.Abstractions;
using Application.Accounts.Commands.Login;
using Application.Accounts.Validators;
using Application.Behaviors;
using Application.Mappings;
using Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(LoginCommand).Assembly);

            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));

            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        services.AddValidatorsFromAssemblyContaining<LoginValidator>();

        services.AddScoped<IUserActivityService, UserActivityService>();

        return services;
    }
}
