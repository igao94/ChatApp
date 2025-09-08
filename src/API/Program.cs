using API.Extensions;
using API.Middleware;
using Application.Extensions;
using Infrastructure.Database;
using Infrastructure.Database.Seed;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, config) =>
{
    config
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
        .WriteTo.Console();
});

builder.Services.AddApiServices();

builder.Services.AddApplicationService();

builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseMiddleware<IsUserActiveMiddleware>();

app.UseAuthorization();

app.MapControllers();

await SeedDatabaseAsync(app);

app.Run();

static async Task SeedDatabaseAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();

    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();

        var useInMemoryDatabase = services.GetRequiredService<IConfiguration>()
            .GetValue<bool>("UseInMemoryDatabase");

        if (!useInMemoryDatabase)
        {
            await context.Database.MigrateAsync();
        }

        var seeder = services.GetRequiredService<ISeedDatabase>();

        await seeder.SeedDatabaseAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();

        logger.LogError(ex, "An error occurred.");
    }
}