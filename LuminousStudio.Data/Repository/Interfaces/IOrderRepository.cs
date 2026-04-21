namespace LuminousStudio.Data.Repository.Interfaces
{
    using Models;

    public interface IOrderRepository
        : IRepository<Order, Guid>, IAsyncRepository<Order, Guid>
    {
    }
}