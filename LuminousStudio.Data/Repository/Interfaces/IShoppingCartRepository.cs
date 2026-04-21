namespace LuminousStudio.Data.Repository.Interfaces
{
    using Models;

    public interface IShoppingCartRepository
        : IRepository<ShoppingCart, Guid>, IAsyncRepository<ShoppingCart, Guid>
    {
    }
}