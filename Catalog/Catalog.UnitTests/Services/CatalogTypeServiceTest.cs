using System.Linq;
using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos.CatalogType;

namespace Catalog.UnitTests.Services;

public class CatalogTypeServiceTest
{
    private readonly ICatalogTypeService _catalogTypeService;

    private readonly Mock<IRepository<CatalogType, CatalogTypeCreateDto, CatalogTypeUpdateDto>> _catalogTypeRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;
    private readonly Mock<IMapper> _mapper;

    private readonly int _testId = 1;

    private readonly CatalogTypeCreateDto _catalogTypeCreateDto = new CatalogTypeCreateDto()
    {
        Type = "Shirt"
    };

    private readonly CatalogTypeUpdateDto _catalogTypeUpdateDto = new CatalogTypeUpdateDto()
    {
        Type = "Shirt"
    };

    private readonly CatalogTypeDto _catalogTypeDtoSuccess = new CatalogTypeDto()
    {
        Type = "Shirt"
    };

    private readonly CatalogType _catalogTypeSuccess = new CatalogType()
    {
        Type = "Shirt"
    };

    public CatalogTypeServiceTest()
    {
        _catalogTypeRepository = new Mock<IRepository<CatalogType, CatalogTypeCreateDto, CatalogTypeUpdateDto>>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();
        _mapper = new Mock<IMapper>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogTypeService = new CatalogTypeService(_dbContextWrapper.Object, _logger.Object, _catalogTypeRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetAllAsync_Success()
    {
        // arrange
        var catalogTypesSuccess = new List<CatalogType>()
        {
            new CatalogType()
            {
                Type = "Shirt",
            },
        };

        _catalogTypeRepository.Setup(s => s.GetAllAsync())
            .ReturnsAsync(catalogTypesSuccess);

        _mapper.Setup(s => s.Map<CatalogTypeDto>(
            It.Is<CatalogType>(i => i.Equals(_catalogTypeSuccess)))).Returns(_catalogTypeDtoSuccess);

        // act
        var result = await _catalogTypeService.GetTypesAsync();

        // assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetAllAsync_Failed()
    {
        // arrange
        List<CatalogType> testResult = new List<CatalogType>();

        _catalogTypeRepository.Setup(s => s.GetAllAsync())
            .ReturnsAsync(testResult);

        // act
        var result = await _catalogTypeService.GetTypesAsync();

        // assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        _catalogTypeRepository.Setup(s => s.AddAsync(
            It.IsAny<CatalogTypeCreateDto>())).ReturnsAsync(_catalogTypeSuccess);

        _mapper.Setup(s => s.Map<CatalogTypeDto>(
            It.Is<CatalogType>(i => i.Equals(_catalogTypeSuccess)))).Returns(_catalogTypeDtoSuccess);

        // act
        var result = await _catalogTypeService.AddTypeAsync(_catalogTypeCreateDto);

        // assert
        result.Should().NotBeNull();
        result?.Type.Should().Be(_catalogTypeDtoSuccess.Type);
        result?.Id.Should().Be(_catalogTypeDtoSuccess.Id);
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        CatalogType? testResult = null;

        _catalogTypeRepository.Setup(s => s.AddAsync(
            It.IsAny<CatalogTypeCreateDto>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogTypeService.AddTypeAsync(_catalogTypeCreateDto);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_Success()
    {
        // arrange
        _catalogTypeRepository.Setup(s => s.UpdateAsync(
            It.IsAny<int>(),
            It.IsAny<CatalogTypeUpdateDto>())).ReturnsAsync(_catalogTypeSuccess);

        _mapper.Setup(s => s.Map<CatalogTypeDto>(
            It.Is<CatalogType>(i => i.Equals(_catalogTypeSuccess)))).Returns(_catalogTypeDtoSuccess);

        // act
        var result = await _catalogTypeService.UpdateTypeAsync(_testId, _catalogTypeUpdateDto);

        // assert
        result.Should().NotBeNull();
        result?.Type.Should().Be(_catalogTypeDtoSuccess.Type);
        result?.Id.Should().Be(_catalogTypeDtoSuccess.Id);
    }

    [Fact]
    public async Task UpdateAsync_Failed()
    {
        // arrange
        CatalogType? testResult = null;

        _catalogTypeRepository.Setup(s => s.UpdateAsync(
            It.IsAny<int>(),
            It.IsAny<CatalogTypeUpdateDto>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogTypeService.UpdateTypeAsync(_testId, _catalogTypeUpdateDto);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_Success()
    {
        // arrange
        _catalogTypeRepository.Setup(s => s.DeleteAsync(
            It.IsAny<int>())).ReturnsAsync(true);

        // act
        var result = await _catalogTypeService.DeleteTypeAsync(_testId);

        // assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_Failed()
    {
        // arrange
        bool? testResult = null;

        _catalogTypeRepository.Setup(s => s.DeleteAsync(
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogTypeService.DeleteTypeAsync(_testId);

        // assert
        result.Should().BeNull();
    }
}
