namespace Domain.Entites;

public sealed class AppUser : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTime LastSeen { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
    public string? About { get; set; }
    public ICollection<AppUserRole> Roles { get; set; } = [];
    public ICollection<Message> MessagesSent { get; set; } = [];
    public ICollection<Message> MessagesReceived { get; set; } = [];
}
