using Shared.Pagination;

namespace Application.Messages;

public sealed class ChatParams : PaginationParams<DateTime?>
{
    public Guid RecipientId { get; set; }
}
