namespace Domain.Entites;

public sealed class AppUser : BaseEntity
{
    public AppUser()
    {

    }

    public AppUser(Guid id,
        DateTime createdAt,
        string name,
        string email,
        byte[] passwordHash,
        byte[] passwordSalt)
    {
        Id = id;
        CreatedAt = createdAt;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }

    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;
    public string? About { get; set; }
    public ICollection<AppUserRole> Roles { get; set; } = [];
}
