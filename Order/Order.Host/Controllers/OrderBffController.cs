using System.Net;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Host.Models.Dtos;
using Order.Host.Services.Interfaces;

namespace Order.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("order")]
[Route(ComponentDefaults.DefaultRoute)]
public class OrderBffController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderBffController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<OrderDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllUserOrders()
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        
        if (userId == null)
        {
            return NotFound();
        } 
        
        var orders = await _orderService.GetAllByUserIdAsync(userId);

        return Ok(orders);
    }
}