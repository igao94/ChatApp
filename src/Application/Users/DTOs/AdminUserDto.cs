namespace Application.Users.DTOs;

public sealed class AdminUserDto : UserDto
{
    public bool IsActive { get; set; }
}
