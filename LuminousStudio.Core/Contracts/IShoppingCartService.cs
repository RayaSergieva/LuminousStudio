namespace LuminousStudio.Core.Contracts
{
    using LuminousStudio.Infrastructure.Data.Entities;

    public interface IShoppingCartService
    {
        Task<IEnumerable<ShoppingCart>> GetCartItemsAsync(Guid userId);
        Task<ShoppingCart?> GetCartItemByIdAsync(Guid cartId);
        Task AddOrUpdateItemAsync(Guid tiffanyLampId, Guid userId);
        Task RemoveAsync(Guid cartId);
        Task UpdateAsync(ShoppingCart cart);
        Task ClearAsync(Guid  userId);
    }
}
