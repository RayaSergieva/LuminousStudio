namespace LuminousStudio.Data.Repository
{
    using Interfaces;
    using Models;

    public class ShoppingCartRepository
        : BaseRepository<ShoppingCart, Guid>, IShoppingCartRepository
    {
        public ShoppingCartRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}