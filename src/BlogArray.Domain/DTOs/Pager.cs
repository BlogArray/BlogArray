namespace BlogArray.Domain.DTOs;

public class PagedResult<T>
{
    public int CurrentPage { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalItemsCount { get; set; }
    public int TotalPageCount { get; set; }
    public List<T>? Items { get; set; }
}