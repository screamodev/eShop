using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Enums;
using Catalog.Host.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize, Dictionary<CatalogFilter, IEnumerable<int>>? filters)
    {
        var query = _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .Include(i => i.CatalogGender)
            .Include(i => i.CatalogItemSizes)
            .AsQueryable();

        if (filters != null)
        {
            foreach (var filter in filters)
            {
                if (filter.Key == CatalogFilter.BrandId)
                {
                    var id = filter.Value.ToList().First();
                    query = query.Where(item => item.CatalogBrand.Id == id);
                }

                if (filter.Key == CatalogFilter.TypeId)
                {
                    var id = filter.Value.ToList().First();
                    query = query.Where(item => item.CatalogType.Id == id);
                }

                if (filter.Key == CatalogFilter.GenderId)
                {
                    var id = filter.Value.ToList().First();
                    query = query.Where(item => item.CatalogGender != null && item.CatalogGender.Id == id);
                }

                if (filter.Key == CatalogFilter.SizeIds)
                {
                    var ids = filter.Value.ToList();

                    foreach (var id in ids)
                    {
                        Console.WriteLine(id);
                    }

                    query = query.Where(item => item.CatalogItemSizes != null && item.CatalogItemSizes.Any(cs => ids.Contains(cs.Id)));
                }
            }
        }

        var totalItems = await _dbContext.CatalogItems
            .LongCountAsync();

        var itemsOnPage = await query
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<CatalogItem?> GetById(int id)
    {
        var item = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .Include(i => i.CatalogGender)
            .Include(i => i.CatalogItemSizes)
            .FirstOrDefaultAsync(item => item.Id == id);

        return item;
    }

    public async Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var item = await _dbContext.AddAsync(new CatalogItem
        {
            CatalogBrandId = catalogBrandId,
            CatalogTypeId = catalogTypeId,
            Description = description,
            Name = name,
            PictureFileName = pictureFileName,
            Price = price
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }
}