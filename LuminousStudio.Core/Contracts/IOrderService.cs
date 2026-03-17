using LuminousStudio.Infrastructure.Data.Entities;

namespace LuminousStudio.Core.Contracts
{
    public interface IOrderService
    {
        bool Create(int tiffanyLampId, string userId, int quantity);

        List<Order> GetOrders();

        List<Order> GetOrdersByUser(string userId);

        Order? GetOrderById(int orderId);

        bool RemoveById(int orderId);

        bool Update(int orderId, int tiffanyLampId, string userId, int quantity);
    }
}