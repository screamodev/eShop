using Catalog.Host.Models.Dtos.CatalogGender;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogGenderService
{
    Task<List<CatalogGenderDto>> GetGenderAsync();
    Task<CatalogGenderDto?> AddGenderAsync(CatalogGenderCreateDto catalogBrand);
    Task<CatalogGenderDto?> UpdateGenderAsync(int id, CatalogGenderUpdateDto catalogBrand);
    Task<bool?> DeleteGenderAsync(int id);
}