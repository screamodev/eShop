using AutoMapper;
using Catalog.Host.Data;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class Repository<TEntity, TCreateDto, TUpdateDto>
    : IRepository<TEntity, TCreateDto, TUpdateDto>
    where TEntity : class, new()
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<Repository<TEntity, TCreateDto, TUpdateDto>> _logger;
    private readonly IMapper _mapper;

    public Repository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        IMapper mapper,
        ILogger<Repository<TEntity, TCreateDto, TUpdateDto>> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        var item = await _dbContext.Set<TEntity>().FindAsync(id);

        if (item == null)
        {
            _logger.LogError($"Item with such id: {id} doesn't exist.");
        }

        return item;
    }

    public async Task<TEntity?> AddAsync(TCreateDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);

        var item = await _dbContext.Set<TEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return item.Entity;
    }

    public async Task<TEntity?> UpdateAsync(int id, TUpdateDto dto)
    {
        var item = await GetByIdAsync(id);
        if (item == null)
        {
            _logger.LogError($"Item with such id: {id} doesn't exist.");
            return null;
        }

        _mapper.Map(dto, item);

        await _dbContext.SaveChangesAsync();
        return item;
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        var item = await GetByIdAsync(id);
        if (item == null)
        {
            _logger.LogError($"Item with such id: {id} doesn't exist.");
            return null;
        }

        _dbContext.Set<TEntity>().Remove(item);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
