using Basket.Host.Models.Requests;
using Basket.Host.Services.Interfaces;
using Infrastructure.Identity;
using Infrastructure.RateLimit.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace Basket.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("basket")]
[Route(ComponentDefaults.DefaultRoute)]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddItem([FromBody] AddBasketItemRequest data)
    {
        var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

        if (basketId == null)
        {
            return NotFound();
        }
        
        await _basketService.AddItem(basketId, data);
        return Ok();
    }
}