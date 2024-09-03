using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos.CatalogBrand;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogBrandService : BaseDataService<ApplicationDbContext>, ICatalogBrandService
{
    private readonly IRepository<CatalogBrand, CatalogBrandCreateDto, CatalogBrandUpdateDto> _catalogBrandRepository;
    private readonly IMapper _mapper;

    public CatalogBrandService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IRepository<CatalogBrand, CatalogBrandCreateDto, CatalogBrandUpdateDto> catalogBrandRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogBrandRepository = catalogBrandRepository;
        _mapper = mapper;
    }

    public async Task<List<CatalogBrandDto>> GetBrandsAsync()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogBrandRepository.GetAllAsync();
            return result.Select(s => _mapper.Map<CatalogBrandDto>(s)).ToList();
        });
    }

    public async Task<CatalogBrandDto?> AddBrandAsync(CatalogBrandCreateDto catalogBrand)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var item = await _catalogBrandRepository.AddAsync(catalogBrand);

            return _mapper.Map<CatalogBrandDto>(item);
        });
    }

    public async Task<CatalogBrandDto?> UpdateBrandAsync(int id, CatalogBrandUpdateDto catalogBrand)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var item = await _catalogBrandRepository.UpdateAsync(id, catalogBrand);
            return item == null ? null : _mapper.Map<CatalogBrandDto>(item);
        });
    }

    public async Task<bool?> DeleteBrandAsync(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            return await _catalogBrandRepository.DeleteAsync(id);
        });
    }
}