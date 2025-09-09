using Shared.Pagination;

namespace Application.Users;

public sealed class UserParams : PaginationParams<DateTime?>
{
    private string? _searchTerm;

    public string SearchTerm
    {
        get => _searchTerm ?? string.Empty;

        set => _searchTerm = value.ToLower();
    }
}
