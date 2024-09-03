namespace Catalog.Host.Models.Requests;

public class PaginatedItemsWithFiltersRequest<T> : PaginatedItemsRequest
    where T : notnull
{
    public Dictionary<T, IEnumerable<int>>? Filters { get; set; }
}