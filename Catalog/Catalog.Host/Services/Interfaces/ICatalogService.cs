using Catalog.Host.Models.Dtos.CatalogItem;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemsAsync(int pageIndex, int pageSize, Dictionary<CatalogFilter, IEnumerable<int>>? filters);
    Task<CatalogItemDto?> GetById(int id);
}