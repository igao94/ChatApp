using Domain.Entites;
using System.Text.Json;

namespace Infrastructure.Database.Seed;

internal sealed class SeedDatabase(AppDbContext context) : ISeedDatabase
{
    public async Task SeedDatabaseAsync()
    {
        if (!context.AppRoles.Any())
        {
            var roles = GetRoles();

            context.AppRoles.AddRange(roles);

            await context.SaveChangesAsync();
        }

        if (!context.Users.Any())
        {
            var users = GetUsers();

            context.Users.AddRange(users);

            await context.SaveChangesAsync();
        }

        if (!context.AppUserRoles.Any())
        {
            var userRoles = GetUserRoles();

            context.AppUserRoles.AddRange(userRoles);

            await context.SaveChangesAsync();
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
