namespace Application.Accounts.DTOs;

public sealed class CurrentUserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}
