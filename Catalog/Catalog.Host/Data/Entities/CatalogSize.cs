namespace Catalog.Host.Data.Entities;

public class CatalogSize
{
    public int Id { get; set; }
    public string? Size { get; set; }
    public List<CatalogItem>? CatalogItems { get; set; }
}