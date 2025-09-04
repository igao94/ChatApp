namespace Domain.Entites;

public sealed class Message : BaseEntity
{
    public string Content { get; set; } = null!;
    public DateTime? DateRead { get; set; }
    public bool SenderDeleted { get; set; }
    public bool RecipientDeleted { get; set; }
    public DateTime? UpdatedAt {  get; set; } 
    public Guid? SenderId { get; set; } // If sender or recipient is deleted messages won't be deleted
    public AppUser? Sender { get; set; } // That's why properties are nullable
    public Guid? RecipientId { get; set; } // Same here
    public AppUser? Recipient { get; set; } // Same here 
}
