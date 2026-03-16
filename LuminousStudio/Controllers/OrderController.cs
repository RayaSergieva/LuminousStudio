using LuminousStudio.Core.Contracts;
using LuminousStudio.Infrastructure.Data.Entities;
using LuminousStudio.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

namespace LuminousStudio.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ITiffanyLampService _tiffanyLampService;
        private readonly IOrderService _orderService;
        private readonly IShoppingCartService _shoppingCartService;

        public OrderController(
            ITiffanyLampService tiffanyLampService,
            IOrderService orderService,
            IShoppingCartService shoppingCartService)
        {
            _tiffanyLampService = tiffanyLampService;
            _orderService = orderService;
            _shoppingCartService = shoppingCartService;
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            List<OrderIndexVM> orders = _orderService.GetOrders()
                .Select(x => new OrderIndexVM
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate.ToString("dd-MMM-yyyy hh:mm", CultureInfo.InvariantCulture),
                    UserId = x.UserId,
                    User = x.User.UserName,
                    TiffanyLampId = x.TiffanyLampId,
                    TiffanyLamp = x.TiffanyLamp.TiffanyLampName,
                    Picture = x.TiffanyLamp.Picture,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    Discount = x.Discount,
                    TotalPrice = x.TotalPrice,
                }).ToList();

            return View(orders);
        }

        public ActionResult Create(int id)
        {
            TiffanyLamp tiffanyLamp = _tiffanyLampService.GetTiffanyLampById(id);
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
        public ActionResult Create(OrderCreateVM bindingModel)
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var tiffanyLamp = this._tiffanyLampService.GetTiffanyLampById(bindingModel.TiffanyLampId);
            if (currentUserId == null || tiffanyLamp == null || tiffanyLamp.Quantity < bindingModel.Quantity || tiffanyLamp.Quantity == 0)
            {
                return RedirectToAction("Denied", "Order");
            }

            if (ModelState.IsValid)
            {
                _orderService.Create(bindingModel.TiffanyLampId, currentUserId, bindingModel.Quantity);
                return this.RedirectToAction("Index", "TiffanyLamp");
            }

            return View(bindingModel);
        }

        [HttpPost]
        public IActionResult CreateFromCart()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = _shoppingCartService.GetCartItems(userId);

            foreach (var cart in cartItems)
            {
                _orderService.Create(cart.TiffanyLampId, userId, cart.Count);
            }

            _shoppingCartService.Clear(userId);

            return RedirectToAction("MyOrders");
        }

        public ActionResult Denied()
        {
            return View();
        }

        public ActionResult MyOrders()
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<OrderIndexVM> orders = _orderService.GetOrdersByUser(currentUserId)
                .Select(x => new OrderIndexVM
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate.ToString("dd-MMM-yyyy hh:mm", CultureInfo.InvariantCulture),
                    UserId = x.UserId,
                    User = x.User.UserName,
                    TiffanyLampId = x.TiffanyLampId,
                    TiffanyLamp = x.TiffanyLamp.TiffanyLampName,
                    Picture = x.TiffanyLamp.Picture,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    Discount = x.Discount,
                    TotalPrice = x.TotalPrice,
                }).ToList();

            return View(orders);
        }
    }
}
