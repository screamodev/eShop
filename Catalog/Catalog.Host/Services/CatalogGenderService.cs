using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos.CatalogGender;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogGenderService : BaseDataService<ApplicationDbContext>, ICatalogGenderService
{
    private readonly IRepository<CatalogGender, CatalogGenderCreateDto, CatalogGenderUpdateDto> _catalogGenderRepository;
    private readonly IMapper _mapper;

    public CatalogGenderService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IRepository<CatalogGender, CatalogGenderCreateDto, CatalogGenderUpdateDto> catalogGenderRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogGenderRepository = catalogGenderRepository;
        _mapper = mapper;
    }

    public async Task<List<CatalogGenderDto>> GetGenderAsync()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogGenderRepository.GetAllAsync();
            return result.Select(s => _mapper.Map<CatalogGenderDto>(s)).ToList();
        });
    }

    public async Task<CatalogGenderDto?> AddGenderAsync(CatalogGenderCreateDto catalogGender)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var item = await _catalogGenderRepository.AddAsync(catalogGender);

            return _mapper.Map<CatalogGenderDto>(item);
        });
    }

    public async Task<CatalogGenderDto?> UpdateGenderAsync(int id, CatalogGenderUpdateDto catalogGender)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var item = await _catalogGenderRepository.UpdateAsync(id, catalogGender);
            return item == null ? null : _mapper.Map<CatalogGenderDto>(item);
        });
    }

    public async Task<bool?> DeleteGenderAsync(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            return await _catalogGenderRepository.DeleteAsync(id);
        });
    }
}