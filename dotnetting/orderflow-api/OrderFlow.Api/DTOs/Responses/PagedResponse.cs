namespace OrderFlow.Api.DTOs.Responses;

public class PagedResponse<T>
{
    public IEnumerable<T> Items { get; set; } = [];

    public int Page { get; set; }

    public int PageSize { get; set; }

    public int TotalItems { get; set; }

    public int TotalPages { get; set; }

    public static PagedResponse<T> Create(
        IEnumerable<T> items,
        int page,
        int pageSize,
        int totalItems)
    {
        return new PagedResponse<T>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
        };
    }
}