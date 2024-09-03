using Catalog.Host.Models.Dtos.CatalogGender;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("catalog.catalogGender")]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogGenderController : ControllerBase
{
    private readonly ICatalogGenderService _catalogGenderService;
    private readonly ILogger<CatalogGenderController> _logger;

    public CatalogGenderController(ICatalogGenderService catalogGenderService, ILogger<CatalogGenderController> logger)
    {
        _catalogGenderService = catalogGenderService;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CatalogGenderDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add([FromQuery] CatalogGenderCreateDto catalogGender)
    {
        var result = await _catalogGenderService.AddGenderAsync(catalogGender);

        if (result == null)
        {
            return StatusCode(500);
        }

        return Ok(result);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(CatalogGenderDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update([FromQuery] CatalogGenderUpdateDto catalogGender, [FromRoute] int id)
    {
        var result = await _catalogGenderService.UpdateGenderAsync(id, catalogGender);

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
        var result = await _catalogGenderService.DeleteGenderAsync(id);

        if (result == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}