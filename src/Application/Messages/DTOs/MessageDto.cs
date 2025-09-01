namespace Application.Messages.DTOs;

public sealed class MessageDto
{
    public Guid Id { get; set; }
    public string SenderName { get; set; } = null!;
    public Guid SenderId { get; set; }
    public string RecipientName { get; set; } = null!;
    public Guid RecipientId { get; set; }
    public string Content { get; set; } = null!;
    public DateTime? DateRead { get; set; }
    public DateTime CreatedAt { get; set; }
}
