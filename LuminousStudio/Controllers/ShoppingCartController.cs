using LuminousStudio.Core.Contracts;
using LuminousStudio.Models.ShoppingCart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LuminousStudio.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _cartService;
        private readonly ITiffanyLampService _lampService;

        public ShoppingCartController(IShoppingCartService cartService, ITiffanyLampService lampService)
        {
            _cartService = cartService;
            _lampService = lampService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var items = _cartService.GetCartItems(userId);

            foreach (var item in items)
            {
                item.Price = item.TiffanyLamp.Price - item.TiffanyLamp.Discount;
            }

            var model = new ShoppingCartVM
            {
                ShoppingCartList = items
            };

            return View(model);
        }

        public IActionResult Add(int tiffanyLampId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var existing = _cartService.GetCartItems(userId)
                .FirstOrDefault(c => c.TiffanyLampId == tiffanyLampId);

            if (existing != null)
            {
                existing.Count++;
                _cartService.Update(existing);
            }
            else
            {
                var lamp = _lampService.GetTiffanyLampById(tiffanyLampId);

                var cartItem = new Infrastructure.Data.Entities.ShoppingCart
                {
                    TiffanyLampId = tiffanyLampId,
                    Count = 1,
                    ApplicationUserId = userId,
                    TiffanyLamp = lamp,
                    Price = lamp.Price - lamp.Discount
                };
                _cartService.Add(cartItem);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Plus(int id)
        {
            var item = _cartService.GetCartItems(User.FindFirstValue(ClaimTypes.NameIdentifier))
                .FirstOrDefault(c => c.Id == id);

            if (item != null)
            {
                item.Count++;
                _cartService.Update(item);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Minus(int id)
        {
            var item = _cartService.GetCartItems(User.FindFirstValue(ClaimTypes.NameIdentifier))
                .FirstOrDefault(c => c.Id == id);

            if (item != null)
            {
                if (item.Count <= 1)
                {
                    _cartService.Remove(id);
                }
                else
                {
                    item.Count--;
                    _cartService.Update(item);
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            _cartService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
