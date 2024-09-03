using System.Net;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Host.Data.Entities;
using Order.Host.Models.Requests;
using Order.Host.Services.Interfaces;

namespace Order.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("order")]
[Route(ComponentDefaults.DefaultRoute)]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(OrderEntity), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddOrder([FromBody] CreateOrderRequest request)
    {
        var customerId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? "0");
        
        var order = await _orderService.AddAsync(request, customerId);

        return Ok(order);
    }
}