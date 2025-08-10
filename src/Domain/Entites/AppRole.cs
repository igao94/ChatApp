namespace Domain.Entites;

public sealed class AppRole : BaseEntity
{
    public AppRole()
    {

    }

    public AppRole(Guid id, DateTime createdAt, string name, string normalizedName)
    {
        Id = id;
        CreatedAt = createdAt;
        Name = name;
        NormalizedName = normalizedName;
    }

    public string Name { get; set; } = null!;
    public string NormalizedName { get; set; } = null!;
    public ICollection<AppUserRole> Users { get; set; } = [];
}
