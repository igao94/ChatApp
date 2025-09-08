namespace Domain.Entites;

public sealed class Message : BaseEntity
{
    public string Content { get; set; } = null!;
    public DateTime? DateRead { get; set; }
    public bool SenderDeleted { get; set; }
    public bool RecipientDeleted { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid SenderId { get; set; }
    public AppUser Sender { get; set; } = null!;
    public Guid RecipientId { get; set; }
    public AppUser Recipient { get; set; } = null!;
}
