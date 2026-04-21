namespace LuminousStudio.Web.Areas.Admin.Controllers
{
    using LuminousStudio.Services.Core.Contracts;
    using LuminousStudio.Web.Common;
    using LuminousStudio.Web.ViewModels.Order;
    using Microsoft.AspNetCore.Mvc;
    using System.Globalization;

    public class OrderManagementController : BaseAdminController
    {
        private readonly IOrderService _orderService;

        public OrderManagementController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            const int pageSize = 10;

            var allOrders = (await _orderService.GetOrdersAsync())
                .Select(o => new OrderIndexVM
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate.ToString("dd-MMM-yyyy hh:mm",
                        CultureInfo.InvariantCulture),
                    UserId = o.UserId,
                    User = o.User.UserName ?? "Unknown",
                    TiffanyLampId = o.TiffanyLampId,
                    TiffanyLamp = o.TiffanyLamp.TiffanyLampName,
                    Picture = o.TiffanyLamp.Picture,
                    Quantity = o.Quantity,
                    Price = o.Price,
                    Discount = o.Discount,
                    TotalPrice = o.TotalPrice
                }).ToList();

            var paginatedOrders = PaginatedList<OrderIndexVM>
                .Create(allOrders, page, pageSize);

            return View(paginatedOrders);
        }
    }
}