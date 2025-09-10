using Application.Users.DTOs;
using Domain.Entites;

namespace Application.Extensions.Mappings;

internal static class UserMappingExtensions
{
    public static UserDto ToDto(this AppUser user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            LastSeen = user.LastSeen,
            About = user.About
        };
    }

    public static AdminUserDto ToAdminDto(this AppUser user)
    {
        return new AdminUserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            LastSeen = user.LastSeen,
            About = user.About
        };
    }
}
