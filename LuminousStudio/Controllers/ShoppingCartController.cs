namespace LuminousStudio.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using LuminousStudio.Core.Contracts;
    using LuminousStudio.Models.ShoppingCart;

    [Authorize]
    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartService _cartService;

        public ShoppingCartController(IShoppingCartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var items = (await _cartService.GetCartItemsAsync(userId.Value)).ToList();

            var model = new ShoppingCartVM
            {
                ShoppingCartList = items
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Guid tiffanyLampId)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            await _cartService.AddOrUpdateItemAsync(tiffanyLampId, userId.Value);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Plus(Guid id)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var item = await _cartService.GetCartItemByIdAsync(id);

            if (item == null || item.ApplicationUserId != userId.Value)
            {
                return NotFound();
            }

            item.Count++;
            await _cartService.UpdateAsync(item);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Minus(Guid id)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var item = await _cartService.GetCartItemByIdAsync(id);
            if (item == null || item.ApplicationUserId != userId.Value)
            {
                return NotFound();
            }

            if (item.Count <= 1)
            {
                await _cartService.RemoveAsync(id);
            }
            else
            {
                item.Count--;
                await _cartService.UpdateAsync(item);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(Guid id)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var item = await _cartService.GetCartItemByIdAsync(id);
            if (item == null || item.ApplicationUserId != userId.Value)
            {
                return NotFound();
            }

            await _cartService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
