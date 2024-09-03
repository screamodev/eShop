namespace Catalog.Host.Models.Response;

public class ItemsResponse<T>
{
    public IEnumerable<T> Data { get; init; } = null!;
}