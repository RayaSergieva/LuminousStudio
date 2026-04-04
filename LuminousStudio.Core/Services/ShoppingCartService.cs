namespace LuminousStudio.Core.Services
{
    using Microsoft.EntityFrameworkCore;

    using LuminousStudio.Core.Contracts;
    using LuminousStudio.Infrastructure.Data;
    using LuminousStudio.Infrastructure.Data.Entities;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShoppingCart>> GetCartItemsAsync(Guid userId)
        {
            var items = await _context.ShoppingCarts
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
            return await _context.ShoppingCarts
                .Include(c => c.TiffanyLamp)
                .FirstOrDefaultAsync(c => c.Id == cartId);
        }

        public async Task AddOrUpdateItemAsync(Guid tiffanyLampId, Guid userId)
        {
            var lamp = await _context.TiffanyLamps.FindAsync(tiffanyLampId);
            if (lamp == null || lamp.Quantity <= 0) 
            {
                return;
            }

            var existing = await _context.ShoppingCarts
                .FirstOrDefaultAsync(c => c.TiffanyLampId == tiffanyLampId
                      && c.ApplicationUserId == userId);

            if (existing != null)
            {
                existing.Count++;
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
                _context.ShoppingCarts.Add(cartItem);
            }

            lamp.Quantity--;  
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid cartId)
        {
            var cart = await _context.ShoppingCarts.FindAsync(cartId);
            if (cart != null)
            {
                _context.ShoppingCarts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(ShoppingCart cart)
        {
            _context.ShoppingCarts.Update(cart);
            await _context.SaveChangesAsync();
        }

        public async Task ClearAsync(Guid userId)
        {
            await _context.ShoppingCarts
                .Where(c => c.ApplicationUserId == userId)
                .ExecuteDeleteAsync();
        }
    }
}
