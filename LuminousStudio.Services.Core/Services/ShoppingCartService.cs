namespace LuminousStudio.Services.Core.Services
{
    using Microsoft.EntityFrameworkCore;

    using LuminousStudio.Data.Models;
    using LuminousStudio.Data.Repository.Interfaces;
    using LuminousStudio.Services.Core.Contracts;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ITiffanyLampRepository _tiffanyLampRepository;

        public ShoppingCartService(
            IShoppingCartRepository shoppingCartRepository,
            ITiffanyLampRepository tiffanyLampRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _tiffanyLampRepository = tiffanyLampRepository;
        }

        public async Task<IEnumerable<ShoppingCart>> GetCartItemsAsync(Guid userId)
        {
            var items = await _shoppingCartRepository
                .GetAllAttached()
                .Include(c => c.TiffanyLamp)
                .Where(c => c.ApplicationUserId == userId)
                .ToListAsync();

            foreach (var item in items)
            {
                item.Price = item.TiffanyLamp.Price - item.TiffanyLamp.Discount;
            }

            return items;
        }

        public async Task<ShoppingCart?> GetCartItemByIdAsync(Guid cartId)
        {
            return await _shoppingCartRepository
                .GetAllAttached()
                .Include(c => c.TiffanyLamp)
                .FirstOrDefaultAsync(c => c.Id == cartId);
        }

        public async Task AddOrUpdateItemAsync(Guid tiffanyLampId, Guid userId)
        {
            var lamp = await _tiffanyLampRepository.GetByIdAsync(tiffanyLampId);
            if (lamp == null)
            {
                return;
            }

            var existing = await _shoppingCartRepository
                .GetAllAttached()
                .FirstOrDefaultAsync(c => c.TiffanyLampId == tiffanyLampId
                    && c.ApplicationUserId == userId);

            if (existing != null)
            {
                existing.Count++;
                await _shoppingCartRepository.UpdateAsync(existing);
            }
            else
            {
                var cartItem = new ShoppingCart
                {
                    TiffanyLampId = tiffanyLampId,
                    Count = 1,
                    ApplicationUserId = userId,
                    Price = lamp.Price - lamp.Discount
                };
                await _shoppingCartRepository.AddAsync(cartItem);
            }
        }

        public async Task RemoveAsync(Guid cartId)
        {
            var cart = await _shoppingCartRepository.GetByIdAsync(cartId);
            if (cart != null)
            {
                await _shoppingCartRepository.DeleteAsync(cart);
            }
        }

        public async Task UpdateAsync(ShoppingCart cart)
        {
            await _shoppingCartRepository.UpdateAsync(cart);
        }

        public async Task ClearAsync(Guid userId)
        {
            await _shoppingCartRepository
                .GetAllAttached()
                .Where(c => c.ApplicationUserId == userId)
                .ExecuteDeleteAsync();
        }
    }
}