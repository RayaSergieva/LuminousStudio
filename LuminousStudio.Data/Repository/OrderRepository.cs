namespace LuminousStudio.Data.Repository
{
    using Interfaces;
    using Models;

    public class OrderRepository
        : BaseRepository<Order, Guid>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}