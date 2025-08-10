﻿namespace Domain.Entites;

public sealed class AppUserRole : BaseEntity
{
    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;
    public Guid RoleId { get; set; }
    public AppRole Role { get; set; } = null!;
}
