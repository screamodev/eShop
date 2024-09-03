using System.Linq;
using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos.CatalogBrand;

namespace Catalog.UnitTests.Services;

public class CatalogBrandServiceTest
{
    private readonly int _testId = 1;
    private readonly CatalogBrandCreateDto _catalogBrandCreateDto = new CatalogBrandCreateDto { Brand = "Gucci" };
    private readonly CatalogBrandUpdateDto _catalogBrandUpdateDto = new CatalogBrandUpdateDto { Brand = "Gucci" };
    private readonly CatalogBrandDto _catalogBrandDtoSuccess = new CatalogBrandDto { Brand = "Gucci" };
    private readonly CatalogBrand _catalogBrandSuccess = new CatalogBrand { Brand = "Gucci" };

    private ICatalogBrandService _catalogBrandService;
    private Mock<IRepository<CatalogBrand, CatalogBrandCreateDto, CatalogBrandUpdateDto>> _catalogBrandRepository;
    private Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private Mock<ILogger<CatalogService>> _logger;
    private Mock<IMapper> _mapper;

    public CatalogBrandServiceTest()
    {
        _catalogBrandRepository = new Mock<IRepository<CatalogBrand, CatalogBrandCreateDto, CatalogBrandUpdateDto>>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();
        _mapper = new Mock<IMapper>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogBrandService = new CatalogBrandService(_dbContextWrapper.Object, _logger.Object, _catalogBrandRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnNonEmptyList_WhenBrandsExist()
    {
        // arrange
        var catalogBrands = new List<CatalogBrand> { _catalogBrandSuccess };
        _catalogBrandRepository.Setup(s => s.GetAllAsync()).ReturnsAsync(catalogBrands);
        _mapper.Setup(m => m.Map<CatalogBrandDto>(It.IsAny<CatalogBrand>())).Returns(_catalogBrandDtoSuccess);

        // act
        var result = await _catalogBrandService.GetBrandsAsync();

        // assert
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result.First().Brand.Should().Be("Gucci");
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoBrandsExist()
    {
        // arrange
        _catalogBrandRepository.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<CatalogBrand>());

        // act
        var result = await _catalogBrandService.GetBrandsAsync();

        // assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task AddAsync_ShouldReturnDto_WhenAdditionIsSuccessful()
    {
        // arrange
        _catalogBrandRepository.Setup(s => s.AddAsync(It.IsAny<CatalogBrandCreateDto>())).ReturnsAsync(_catalogBrandSuccess);
        _mapper.Setup(m => m.Map<CatalogBrandDto>(It.IsAny<CatalogBrand>())).Returns(_catalogBrandDtoSuccess);

        // act
        var result = await _catalogBrandService.AddBrandAsync(_catalogBrandCreateDto);

        // assert
        result.Should().NotBeNull();
        result?.Brand.Should().Be(_catalogBrandDtoSuccess.Brand);
    }

    [Fact]
    public async Task AddAsync_ShouldReturnNull_WhenAdditionFails()
    {
        // arrange
        CatalogBrand? nullResult = null;
        _catalogBrandRepository.Setup(s => s.AddAsync(It.IsAny<CatalogBrandCreateDto>())).ReturnsAsync(nullResult);

        // act
        var result = await _catalogBrandService.AddBrandAsync(_catalogBrandCreateDto);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnDto_WhenUpdateIsSuccessful()
    {
        // arrange
        _catalogBrandRepository.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<CatalogBrandUpdateDto>())).ReturnsAsync(_catalogBrandSuccess);
        _mapper.Setup(m => m.Map<CatalogBrandDto>(It.IsAny<CatalogBrand>())).Returns(_catalogBrandDtoSuccess);

        // act
        var result = await _catalogBrandService.UpdateBrandAsync(_testId, _catalogBrandUpdateDto);

        // assert
        result.Should().NotBeNull();
        result?.Brand.Should().Be(_catalogBrandDtoSuccess.Brand);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnNull_WhenUpdateFails()
    {
        // arrange
        CatalogBrand? nullResult = null;
        _catalogBrandRepository.Setup(s => s.UpdateAsync(It.IsAny<int>(), It.IsAny<CatalogBrandUpdateDto>())).ReturnsAsync(nullResult);

        // act
        var result = await _catalogBrandService.UpdateBrandAsync(_testId, _catalogBrandUpdateDto);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenDeletionIsSuccessful()
    {
        // arrange
        _catalogBrandRepository.Setup(s => s.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);

        // act
        var result = await _catalogBrandService.DeleteBrandAsync(_testId);

        // assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenDeletionFails()
    {
        // arrange
        _catalogBrandRepository.Setup(s => s.DeleteAsync(It.IsAny<int>())).ReturnsAsync(false);

        // act
        var result = await _catalogBrandService.DeleteBrandAsync(_testId);

        // assert
        result.Should().BeFalse();
    }
}