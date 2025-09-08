using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

internal sealed class MessageEntityConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.Property(m => m.Content).IsRequired();

        builder.HasIndex(m => m.Content);

        builder.HasIndex(m => new { m.RecipientId, m.SenderId, m.RecipientDeleted, m.CreatedAt });

        builder.HasIndex(m => new { m.SenderId, m.RecipientId, m.SenderDeleted, m.CreatedAt });

        builder.HasQueryFilter(m => m.Sender.IsActive && m.Recipient.IsActive);

        builder.HasOne(m => m.Sender)
            .WithMany(u => u.MessagesSent)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(m => m.Recipient)
            .WithMany(u => u.MessagesReceived)
            .HasForeignKey(m => m.RecipientId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
