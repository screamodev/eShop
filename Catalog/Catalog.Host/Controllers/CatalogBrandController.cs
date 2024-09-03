using Catalog.Host.Models.Dtos.CatalogBrand;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("catalog.catalogBrand")]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBrandController : ControllerBase
{
    private readonly ICatalogBrandService _catalogBrandService;
    private readonly ILogger<CatalogBrandController> _logger;

    public CatalogBrandController(ICatalogBrandService catalogBrandService, ILogger<CatalogBrandController> logger)
    {
        _catalogBrandService = catalogBrandService;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CatalogBrandDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add([FromQuery] CatalogBrandCreateDto catalogBrand)
    {
        var result = await _catalogBrandService.AddBrandAsync(catalogBrand);

        if (result == null)
        {
            return StatusCode(500);
        }

        return Ok(result);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(CatalogBrandDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update([FromQuery] CatalogBrandUpdateDto catalogBrand, [FromRoute] int id)
    {
        var result = await _catalogBrandService.UpdateBrandAsync(id, catalogBrand);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _catalogBrandService.DeleteBrandAsync(id);

        if (result == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}