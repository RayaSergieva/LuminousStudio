using LuminousStudio.Infrastructure.Data.Entities;

namespace LuminousStudio.Core.Contracts
{
    public interface IShoppingCartService
    {
        IEnumerable<ShoppingCart> GetCartItems(string userId);
        void Add(ShoppingCart cart);
        void Remove(int cartId);
        void Update(ShoppingCart cart);
        void Clear(string userId);
    }
}
