using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos.CatalogSize;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogSizeService : BaseDataService<ApplicationDbContext>, ICatalogSizeService
{
    private readonly IRepository<CatalogSize, CatalogSizeCreateDto, CatalogSizeUpdateDto> _catalogSizeRepository;
    private readonly IMapper _mapper;

    public CatalogSizeService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IRepository<CatalogSize, CatalogSizeCreateDto, CatalogSizeUpdateDto> catalogSizeRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogSizeRepository = catalogSizeRepository;
        _mapper = mapper;
    }

    public async Task<List<CatalogSizeDto>> GetSizeAsync()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogSizeRepository.GetAllAsync();
            return result.Select(s => _mapper.Map<CatalogSizeDto>(s)).ToList();
        });
    }

    public async Task<CatalogSizeDto?> AddSizeAsync(CatalogSizeCreateDto catalogSize)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var item = await _catalogSizeRepository.AddAsync(catalogSize);

            return _mapper.Map<CatalogSizeDto>(item);
        });
    }

    public async Task<CatalogSizeDto?> UpdateSizeAsync(int id, CatalogSizeUpdateDto catalogSize)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var item = await _catalogSizeRepository.UpdateAsync(id, catalogSize);
            return item == null ? null : _mapper.Map<CatalogSizeDto>(item);
        });
    }

    public async Task<bool?> DeleteSizeAsync(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            return await _catalogSizeRepository.DeleteAsync(id);
        });
    }
}