using Domain.Entites;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Database.Seed;

internal sealed class SeedDatabase(AppDbContext context, ILogger<SeedDatabase> logger) : ISeedDatabase
{
    public async Task SeedDatabaseAsync()
    {
        logger.LogInformation("Starting database seeding...");

        int totalSeeded = 0;

        try
        {
            if (!context.AppRoles.Any())
            {
                logger.LogInformation("Seeding roles...");

                var roles = GetRoles();

                context.AppRoles.AddRange(roles);

                await context.SaveChangesAsync();

                totalSeeded += roles.Count;

                logger.LogInformation("Seeded {count} roles.", roles.Count);
            }
            else
            {
                logger.LogInformation("Roles already exist, skipping seeding roles.");
            }

            if (!context.Users.Any())
            {
                logger.LogInformation("Seeding users...");

                var users = GetUsers();

                context.Users.AddRange(users);

                await context.SaveChangesAsync();

                totalSeeded += users.Count;

                logger.LogInformation("Seeded {count} users.", users.Count);
            }
            else
            {
                logger.LogInformation("Users already exist, skipping seeding users.");
            }

            if (!context.AppUserRoles.Any())
            {
                logger.LogInformation("Seeding user roles...");

                var userRoles = GetUserRoles();

                context.AppUserRoles.AddRange(userRoles);

                await context.SaveChangesAsync();

                totalSeeded += userRoles.Count;

                logger.LogInformation("Seeded {count} user roles.", userRoles.Count);
            }
            else
            {
                logger.LogInformation("UserRoles already exist, skipping seeding user roles.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred during database seeding.");

            throw;
        }

        if (totalSeeded > 0)
        {
            logger.LogInformation("Database seeding completed successfully. " +
                "Total records seeded: {totalSeeded}.", totalSeeded);
        }
        else
        {
            logger.LogInformation("Database seeding completed. " +
                "No new records were added, all data already exists.");
        }
    }

    private static List<AppUser> GetUsers()
    {
        var usersData = File.ReadAllText("../Infrastructure/Database/Seed/Users.json");

        var userSeedDto = JsonSerializer.Deserialize<List<UserSeedDto>>(usersData)
            ?? throw new Exception("Failed to deserialize Users.json");

        var users = userSeedDto.Select(dto => new AppUser
        {
            Id = dto.Id,
            Name = dto.Name,
            Email = dto.Email,
            About = dto.About,
            PasswordHash = Convert.FromBase64String(dto.PasswordHash),
            PasswordSalt = Convert.FromBase64String(dto.PasswordSalt)
        }).ToList();

        return users;
    }

    private static List<AppRole> GetRoles()
    {
        var rolesData = File.ReadAllText("../Infrastructure/Database/Seed/Roles.json");

        var roles = JsonSerializer.Deserialize<List<AppRole>>(rolesData)
            ?? throw new Exception("Failed to deserialize Roles.json");

        return roles;
    }

    private static List<AppUserRole> GetUserRoles()
    {
        var userRolesData = File.ReadAllText("../Infrastructure/Database/Seed/UserRoles.json");

        var userRoles = JsonSerializer.Deserialize<List<AppUserRole>>(userRolesData)
            ?? throw new Exception("Failed to deserialize UsersRoles.json");

        return userRoles;
    }
}
