using Application.Accounts.Commands.Login;
using Application.Accounts.Validators;
using Application.Behaviors;
using Application.Mappings;
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
        });

        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        services.AddValidatorsFromAssemblyContaining<LoginValidator>();

        return services;
    }
}
