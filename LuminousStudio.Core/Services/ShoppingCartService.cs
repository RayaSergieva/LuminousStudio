using LuminousStudio.Core.Contracts;
using LuminousStudio.Infrastructure.Data;
using LuminousStudio.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuminousStudio.Core.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ShoppingCart> GetCartItems(string userId)
            => _context.ShoppingCarts
                       .Include(c => c.TiffanyLamp)
                       .Where(c => c.ApplicationUserId == userId)
                       .ToList();

        public void Add(ShoppingCart cart)
        {
            _context.ShoppingCarts.Add(cart);
            _context.SaveChanges();
        }

        public void Remove(int cartId)
        {
            var cart = _context.ShoppingCarts.Find(cartId);
            if (cart != null)
            {
                _context.ShoppingCarts.Remove(cart);
                _context.SaveChanges();
            }
        }

        public void Update(ShoppingCart cart)
        {
            _context.ShoppingCarts.Update(cart);
            _context.SaveChanges();
        }

        public void Clear(string userId)
        {
            var items = _context.ShoppingCarts.Where(c => c.ApplicationUserId == userId).ToList();
            _context.ShoppingCarts.RemoveRange(items);
            _context.SaveChanges();
        }
    }
}
