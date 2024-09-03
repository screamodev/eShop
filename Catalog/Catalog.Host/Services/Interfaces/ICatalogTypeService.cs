using Catalog.Host.Models.Dtos.CatalogType;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogTypeService
{
    Task<List<CatalogTypeDto>> GetTypesAsync();
    Task<CatalogTypeDto?> AddTypeAsync(CatalogTypeCreateDto catalogType);
    Task<CatalogTypeDto?> UpdateTypeAsync(int id, CatalogTypeUpdateDto catalogType);
    Task<bool?> DeleteTypeAsync(int id);
}