namespace MVC.Dtos;

public class PaginatedItemsRequest<T> where T : notnull
{
    public int PageIndex { get; set; }

    public int PageSize { get; set; }
    
    public Dictionary<T, IEnumerable<int>>? Filters { get; set; }
}