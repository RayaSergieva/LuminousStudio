namespace LuminousStudio.Core.Contracts
{
    using LuminousStudio.Infrastructure.Data.Entities;

    public interface IOrderService
    {
        Task<bool> CreateAsync(Guid tiffanyLampId, Guid userId, int quantity);

        Task<List<Order>> GetOrdersAsync();

        Task<List<Order>> GetOrdersByUserAsync(Guid userId);

        Task<Order?> GetOrderByIdAsync(Guid orderId);

        Task<bool> RemoveByIdAsync(Guid orderId);

        Task<bool> UpdateAsync(Guid orderId, Guid tiffanyLampId, Guid userId, int quantity);

        Task<bool> UserHasOrdersAsync(Guid userId);
    }
}