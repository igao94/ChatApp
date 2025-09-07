namespace Application.Users.DTOs;

public sealed class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime LastSeen { get; set; } = DateTime.UtcNow;
    public string? About { get; set; }
}
