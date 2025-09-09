using Shared.Pagination;

namespace Application.Messages;

public sealed class SearchChatParams : PaginationParams<DateTime?>
{
    public Guid RecipientId { get; set; }

    private string? _searchTerm;

    public string? SearchTerm
    {
        get => _searchTerm;

        set
        {
            if (value is null)
            {
                _searchTerm = null;
            }
            else
            {
                _searchTerm = value.ToLower();
            }
        }
    }
}
