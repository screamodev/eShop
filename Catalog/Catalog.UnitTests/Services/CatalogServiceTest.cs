using System.Linq;
using System.Threading;
using Castle.Core.Internal;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Dtos.CatalogItem;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Response;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace Catalog.UnitTests.Services;

public class CatalogServiceTest
{
    private readonly ICatalogService _catalogService;
    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;
    private readonly ITestOutputHelper _testOutputHelper;

    public CatalogServiceTest(ITestOutputHelper output)
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _mapper = new Mock<IMapper>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();
        _testOutputHelper = output;

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Success()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 4;
        var testTotalCount = 12;

        var pagingPaginatedItemsSuccess = new PaginatedItems<CatalogItem>()
        {
            Data = new List<CatalogItem>()
            {
                new CatalogItem()
                {
                    Name = "TestName",
                },
            },
            TotalCount = testTotalCount,
        };

        var catalogItemSuccess = new CatalogItem()
        {
            Name = "TestName"
        };

        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            Name = "TestName"
        };

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize),
            It.Is<Dictionary<CatalogFilter, IEnumerable<int>>?>(i => i == null))).ReturnsAsync(pagingPaginatedItemsSuccess);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageIndex, testPageSize, null);

        _testOutputHelper.WriteLine(JsonConvert.SerializeObject(result));

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
        result?.PageIndex.Should().Be(testPageIndex);
        result?.PageSize.Should().Be(testPageSize);
        result?.Data.FirstOrDefault()?.Name.Should().Be("TestName");
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Failed()
    {
        // arrange
        var testPageIndex = 1000;
        var testPageSize = 10000;

        var filtersTest = new Dictionary<CatalogFilter, IEnumerable<int>>()
        {
            { CatalogFilter.BrandId, new List<int>() { 1 } }
        };

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize),
            It.Is<Dictionary<CatalogFilter, IEnumerable<int>>>(i => i.ContainsKey(CatalogFilter.BrandId)))).Returns((Func<PaginatedItemsResponse<CatalogItemDto>>)null!);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageIndex, testPageSize, filtersTest);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogItemsAsync_NoData_ReturnsEmpty()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 4;

        var pagingPaginatedItemsEmpty = new PaginatedItems<CatalogItem>()
        {
            Data = new List<CatalogItem>(),
            TotalCount = 0,
        };

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize),
            It.IsAny<Dictionary<CatalogFilter, IEnumerable<int>>?>())).ReturnsAsync(pagingPaginatedItemsEmpty);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageIndex, testPageSize, null);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().BeEmpty();
        result?.Count.Should().Be(0);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_InvalidPageSize_ReturnsNull()
    {
        // arrange
        var testPageIndex = 0;
        var invalidPageSize = -1;

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == invalidPageSize),
            It.Is<Dictionary<CatalogFilter, IEnumerable<int>>>(i => i.ContainsKey(CatalogFilter.BrandId)))).Returns((Func<PaginatedItemsResponse<CatalogItemDto>>)null!);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageIndex, invalidPageSize, null);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogItemsAsync_InvalidFilters_ReturnsNull()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 4;

        var invalidFilters = new Dictionary<CatalogFilter, IEnumerable<int>>()
        {
            { (CatalogFilter)999, new List<int>() { 1 } } // Invalid filter key
        };

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize),
            It.Is<Dictionary<CatalogFilter, IEnumerable<int>>>(i => i == invalidFilters))).Returns((Func<PaginatedItemsResponse<CatalogItemDto>>)null!);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageIndex, testPageSize, invalidFilters);

        // assert
        result.Should().BeNull();
    }
}
