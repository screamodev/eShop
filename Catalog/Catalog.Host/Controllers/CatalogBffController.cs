using Catalog.Host.Models.Dtos.CatalogBrand;
using Catalog.Host.Models.Dtos.CatalogGender;
using Catalog.Host.Models.Dtos.CatalogItem;
using Catalog.Host.Models.Dtos.CatalogSize;
using Catalog.Host.Models.Dtos.CatalogType;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBffController : ControllerBase
{
    private readonly ICatalogService _catalogService;
    private readonly ICatalogBrandService _catalogBrandService;
    private readonly ICatalogTypeService _catalogTypeService;
    private readonly ICatalogGenderService _catalogGenderService;
    private readonly ICatalogSizeService _catalogSizeService;
    private readonly ILogger<CatalogBffController> _logger;

    public CatalogBffController(
        ICatalogService catalogService,
        ICatalogBrandService catalogBrandService,
        ICatalogTypeService catalogTypeService,
        ICatalogGenderService catalogGenderService,
        ICatalogSizeService catalogSizeService,
        ILogger<CatalogBffController> logger)
    {
        _catalogService = catalogService;
        _catalogBrandService = catalogBrandService;
        _catalogTypeService = catalogTypeService;
        _catalogGenderService = catalogGenderService;
        _catalogSizeService = catalogSizeService;
        _logger = logger;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Items(PaginatedItemsWithFiltersRequest<CatalogFilter> request)
    {
        var result = await _catalogService.GetCatalogItemsAsync(request.PageIndex, request.PageSize, request.Filters);

        if (result == null)
        {
          return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CatalogItemDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ItemById(int id)
    {
        var result = await _catalogService.GetById(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<CatalogBrandDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Brands()
    {
        var result = await _catalogBrandService.GetBrandsAsync();

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<CatalogTypeDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Types()
    {
        var result = await _catalogTypeService.GetTypesAsync();

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<CatalogGenderDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Genders()
    {
        var result = await _catalogGenderService.GetGenderAsync();

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<CatalogSizeDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Sizes()
    {
        var result = await _catalogSizeService.GetSizeAsync();

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}