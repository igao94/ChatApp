namespace Shared.Pagination;

public sealed class CursorPagination<T, TCursor>
{
    public IReadOnlyList<T> Items { get; set; } = [];
    public TCursor? NextCursor { get; set; }
}
