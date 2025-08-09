using Domain.Entites;
using System.Text.Json;

namespace Infrastructure.Database.Seed;

internal sealed class SeedDatabase(AppDbContext context) : ISeedDatabase
{
    public async Task SeedDatabaseAsync()
    {
        if (!context.Users.Any())
        {
            var users = GetUsers();

            context.Users.AddRange(users);

            await context.SaveChangesAsync();
        }
    }

    private static List<AppUser> GetUsers()
    {
        var jsonData = File.ReadAllText("../Infrastructure/Database/Seed/Users.json");

        var userSeedDto = JsonSerializer.Deserialize<List<UserSeedDto>>(jsonData);

        List<AppUser> users = [];

        foreach (var dto in userSeedDto!)
        {
            var user = new AppUser
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email,
                About = dto.About,
                PasswordHash = Convert.FromBase64String(dto.PasswordHash),
                PasswordSalt = Convert.FromBase64String(dto.PasswordSalt)
            };

            users.Add(user);
        }

        return users;
    }
}
