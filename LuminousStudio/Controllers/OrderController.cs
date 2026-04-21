namespace LuminousStudio.Web.Controllers
{
    using System.Globalization;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using LuminousStudio.Services.Core.Contracts;
    using LuminousStudio.Services.Common;
    using LuminousStudio.Data.Models;
    using LuminousStudio.Web.ViewModels.Order;

    [Authorize]
    public class OrderController : BaseController
    {
        private readonly ITiffanyLampService _tiffanyLampService;
        private readonly IOrderService _orderService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IStockHubService _stockHubService;

        public OrderController(
            ITiffanyLampService tiffanyLampService,
            IOrderService orderService,
            IShoppingCartService shoppingCartService,
            IStockHubService stockHubService)
        {
            _tiffanyLampService = tiffanyLampService;
            _orderService = orderService;
            _shoppingCartService = shoppingCartService;
            _stockHubService = stockHubService;
        }

        [HttpGet]
        [Authorize(Roles = ApplicationRoles.Administrator)]
        public async Task<ActionResult> Index()
        {
            List<OrderIndexVM> orders = (await _orderService.GetOrdersAsync())
                .Select(MapToOrderVM)
                .ToList();

            return View(orders);
        }

        [HttpGet]
        public async Task<ActionResult> Create(Guid id)
        {
            TiffanyLamp? tiffanyLamp = await _tiffanyLampService.GetTiffanyLampByIdAsync(id);
            if (tiffanyLamp == null)
            {
                return NotFound();
            }

            OrderCreateVM order = new OrderCreateVM()
            {
                TiffanyLampId = tiffanyLamp.Id,
                TiffanyLampName = tiffanyLamp.TiffanyLampName,
                QuantityInStock = tiffanyLamp.Quantity,
                Price = tiffanyLamp.Price,
                Discount = tiffanyLamp.Discount,
                Picture = tiffanyLamp.Picture,
            };

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrderCreateVM bindingModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bindingModel);
            }

            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Unauthorized();
            }

            TiffanyLamp? tiffanyLamp = await _tiffanyLampService
                .GetTiffanyLampByIdAsync(bindingModel.TiffanyLampId);

            if (tiffanyLamp == null || tiffanyLamp.Quantity < bindingModel.Quantity)
            {
                return RedirectToAction(nameof(Denied));
            }

            await _orderService.CreateAsync(
                bindingModel.TiffanyLampId, currentUserId.Value, bindingModel.Quantity);

            var updatedLamp = await _tiffanyLampService
                .GetTiffanyLampByIdAsync(bindingModel.TiffanyLampId);

            if (updatedLamp != null)
            {
                await _stockHubService.NotifyStockUpdateAsync(
                    updatedLamp.Id,
                    updatedLamp.Quantity,
                    updatedLamp.TiffanyLampName);
            }

            return RedirectToAction(nameof(Index), "TiffanyLamp");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromCart()
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var cartItems = (await _shoppingCartService
                .GetCartItemsAsync(userId.Value)).ToList();

            if (!cartItems.Any())
            {
                return RedirectToAction(nameof(Index), "ShoppingCart");
            }

            foreach (var cart in cartItems)
            {
                var lamp = await _tiffanyLampService
                    .GetTiffanyLampByIdAsync(cart.TiffanyLampId);
                if (lamp == null || lamp.Quantity < cart.Count)
                {
                    return RedirectToAction(nameof(Denied));
                }
            }

            foreach (var cart in cartItems)
            {
                await _orderService.CreateAsync(
                    cart.TiffanyLampId, userId.Value, cart.Count);

                var updatedLamp = await _tiffanyLampService
                    .GetTiffanyLampByIdAsync(cart.TiffanyLampId);

                if (updatedLamp != null)
                {
                    await _stockHubService.NotifyStockUpdateAsync(
                        updatedLamp.Id,
                        updatedLamp.Quantity,
                        updatedLamp.TiffanyLampName);
                }
            }

            await _shoppingCartService.ClearAsync(userId.Value);

            return RedirectToAction(nameof(MyOrders));
        }

        [HttpGet]
        public ActionResult Denied()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> MyOrders()
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Unauthorized();
            }

            List<OrderIndexVM> orders = (await _orderService
                .GetOrdersByUserAsync(currentUserId.Value))
                .Select(MapToOrderVM)
                .ToList();

            return View(orders);
        }

        private static OrderIndexVM MapToOrderVM(Order x) => new OrderIndexVM
        {
            Id = x.Id,
            OrderDate = x.OrderDate.ToString(
                "dd-MMM-yyyy hh:mm", CultureInfo.InvariantCulture),
            UserId = x.UserId,
            User = x.User.UserName ?? "Unknown",
            TiffanyLampId = x.TiffanyLampId,
            TiffanyLamp = x.TiffanyLamp.TiffanyLampName,
            Picture = x.TiffanyLamp.Picture,
            Quantity = x.Quantity,
            Price = x.Price,
            Discount = x.Discount,
            TotalPrice = x.TotalPrice,
        };
    }
}