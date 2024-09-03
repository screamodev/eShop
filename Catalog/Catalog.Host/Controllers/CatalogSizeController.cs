using Catalog.Host.Models.Dtos.CatalogSize;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("catalog.catalogSize")]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogSizeController : ControllerBase
{
    private readonly ICatalogSizeService _catalogSizeService;
    private readonly ILogger<CatalogSizeController> _logger;

    public CatalogSizeController(ICatalogSizeService catalogSizeService, ILogger<CatalogSizeController> logger)
    {
        _catalogSizeService = catalogSizeService;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CatalogSizeDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add([FromQuery] CatalogSizeCreateDto catalogSize)
    {
        var result = await _catalogSizeService.AddSizeAsync(catalogSize);

        if (result == null)
        {
            return StatusCode(500);
        }

        return Ok(result);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(CatalogSizeDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update([FromQuery] CatalogSizeUpdateDto catalogSize, [FromRoute] int id)
    {
        var result = await _catalogSizeService.UpdateSizeAsync(id, catalogSize);

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
        var result = await _catalogSizeService.DeleteSizeAsync(id);

        if (result == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}