using Shared.Pagination;

namespace Application.Users;

public sealed class UserParams : PaginationParams<DateTime?>
{
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
