using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos.CatalogType;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogTypeService : BaseDataService<ApplicationDbContext>, ICatalogTypeService
{
    private readonly IRepository<CatalogType, CatalogTypeCreateDto, CatalogTypeUpdateDto> _catalogTypeRepository;
    private readonly IMapper _mapper;

    public CatalogTypeService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IRepository<CatalogType, CatalogTypeCreateDto, CatalogTypeUpdateDto> catalogTypeRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogTypeRepository = catalogTypeRepository;
        _mapper = mapper;
    }

    public async Task<List<CatalogTypeDto>> GetTypesAsync()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogTypeRepository.GetAllAsync();
            return result.Select(s => _mapper.Map<CatalogTypeDto>(s)).ToList();
        });
    }

    public async Task<CatalogTypeDto?> AddTypeAsync(CatalogTypeCreateDto catalogType)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var item = await _catalogTypeRepository.AddAsync(catalogType);

            return _mapper.Map<CatalogTypeDto>(item);
        });
    }

    public async Task<CatalogTypeDto?> UpdateTypeAsync(int id, CatalogTypeUpdateDto catalogType)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var item = await _catalogTypeRepository.UpdateAsync(id, catalogType);

            return _mapper.Map<CatalogTypeDto>(item);
        });
    }

    public async Task<bool?> DeleteTypeAsync(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            return await _catalogTypeRepository.DeleteAsync(id);
        });
    }
}