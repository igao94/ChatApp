using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

internal sealed class AppUserEntityConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(u => u.Name).IsRequired();

        builder.Property(u => u.Email).IsRequired();

        builder.Property(u => u.PasswordHash).IsRequired();

        builder.HasIndex(u => u.Email);
    }
}
