using Shared.Pagination;

namespace Application.Messages;

public sealed class MessageParams : PaginationParams<DateTime?>
{
    public string? Container { get; set; }
}
