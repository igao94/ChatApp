namespace Application.Users.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime LastSeen { get; set; }
    public string? About { get; set; }
}
