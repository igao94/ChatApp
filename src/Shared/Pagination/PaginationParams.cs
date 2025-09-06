namespace Shared.Pagination;

public class PaginationParams<TCursor>
{
    private const int MaxPageSize = 20;
    public TCursor? Cursor { get; set; }

    private int _pageSize = 3;

    public int PageSize
    {
        get => _pageSize;

        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}
