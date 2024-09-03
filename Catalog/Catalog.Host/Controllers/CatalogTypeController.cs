using Catalog.Host.Models.Dtos.CatalogType;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("catalog.catalogType")]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogTypeController : ControllerBase
{
    private readonly ILogger<CatalogTypeController> _logger;
    private readonly ICatalogTypeService _catalogTypeService;

    public CatalogTypeController(ICatalogTypeService catalogTypeService, ILogger<CatalogTypeController> logger)
    {
        _catalogTypeService = catalogTypeService;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CatalogTypeDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add([FromQuery] CatalogTypeCreateDto catalogType)
    {
        var result = await _catalogTypeService.AddTypeAsync(catalogType);

        if (result == null)
        {
            return StatusCode(500);
        }

        return Ok(result);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(CatalogTypeDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update([FromQuery] CatalogTypeUpdateDto catalogType, [FromRoute] int id)
    {
        var result = await _catalogTypeService.UpdateTypeAsync(id, catalogType);

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
        var result = await _catalogTypeService.DeleteTypeAsync(id);

        if (result == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}