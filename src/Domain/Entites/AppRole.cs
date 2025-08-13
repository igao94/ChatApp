namespace Domain.Entites;

public sealed class AppRole : BaseEntity
{
    public string Name { get; set; } = null!;
    public string NormalizedName { get; set; } = null!;
    public ICollection<AppUserRole> Users { get; set; } = [];
}
