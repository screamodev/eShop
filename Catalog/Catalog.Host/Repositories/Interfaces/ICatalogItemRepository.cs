using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Enums;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemRepository
{
    Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize, Dictionary<CatalogFilter, IEnumerable<int>>? filters);
    Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<CatalogItem?> GetById(int id);
}