namespace LuminousStudio.WebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using LuminousStudio.Services.Core.Contracts;
    using LuminousStudio.Services.Common;

    public class OrdersApiController : BaseApiController
    {
        private readonly IOrderService _orderService;

        public OrdersApiController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Authorize(Roles = ApplicationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetOrdersAsync();

            var result = orders.Select(o => new
            {
                o.Id,
                o.OrderDate,
                User = o.User.UserName,
                TiffanyLamp = o.TiffanyLamp.TiffanyLampName,
                o.Quantity,
                o.Price,
                o.Discount,
                o.TotalPrice
            });

            return Ok(result);
        }

        [HttpGet("my")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var orders = await _orderService.GetOrdersByUserAsync(userId.Value);

            var result = orders.Select(o => new
            {
                o.Id,
                o.OrderDate,
                TiffanyLamp = o.TiffanyLamp.TiffanyLampName,
                o.Quantity,
                o.Price,
                o.Discount,
                o.TotalPrice
            });

            return Ok(result);
        }
    }
}