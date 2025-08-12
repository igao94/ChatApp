using Application.Abstractions.Repositories;
using Infrastructure.Database;
using Infrastructure.Database.Seed;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            bool useInMemoryDatabase = config.GetValue<bool>("UseInMemoryDatabase");

            if (useInMemoryDatabase)
            {
                opt.UseInMemoryDatabase("InMemoryDatabase").EnableSensitiveDataLogging();
            }
            else
            {
                opt.UseSqlServer(config.GetConnectionString("SqlServer")).EnableSensitiveDataLogging();
            }
        });

        services.AddScoped<ISeedDatabase, SeedDatabase>();

        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

        return services;
    }
}
