using Catalog.Host.Models.Dtos.CatalogSize;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogSizeService
{
    Task<List<CatalogSizeDto>> GetSizeAsync();
    Task<CatalogSizeDto?> AddSizeAsync(CatalogSizeCreateDto catalogBrand);
    Task<CatalogSizeDto?> UpdateSizeAsync(int id, CatalogSizeUpdateDto catalogBrand);
    Task<bool?> DeleteSizeAsync(int id);
}