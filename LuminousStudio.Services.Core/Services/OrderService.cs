namespace LuminousStudio.Services.Core.Services
{
    using Microsoft.EntityFrameworkCore;

    using LuminousStudio.Data.Models;
    using LuminousStudio.Data.Repository.Interfaces;
    using LuminousStudio.Services.Core.Contracts;

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ITiffanyLampRepository _tiffanyLampRepository;

        public OrderService(
            IOrderRepository orderRepository,
            ITiffanyLampRepository tiffanyLampRepository)
        {
            _orderRepository = orderRepository;
            _tiffanyLampRepository = tiffanyLampRepository;
        }

        public async Task<bool> CreateAsync(Guid tiffanyLampId, Guid userId, int quantity)
        {
            var tiffanyLamp = await _tiffanyLampRepository.GetByIdAsync(tiffanyLampId);

            if (tiffanyLamp == null || userId == Guid.Empty ||
                quantity <= 0 || tiffanyLamp.Quantity < quantity)
            {
                return false;
            }

            Order item = new Order
            {
                OrderDate = DateTime.UtcNow,
                TiffanyLampId = tiffanyLampId,
                UserId = userId,
                Quantity = quantity,
                Price = tiffanyLamp.Price,
                Discount = tiffanyLamp.Discount
            };

            tiffanyLamp.Quantity -= quantity;
            await _tiffanyLampRepository.UpdateAsync(tiffanyLamp);
            await _orderRepository.AddAsync(item);

            return true;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _orderRepository
                .GetAllAttached()
                .Include(x => x.User)
                .Include(x => x.TiffanyLamp)
                .OrderByDescending(x => x.OrderDate)
                .ToListAsync();
        }

        public async Task<List<Order>> GetOrdersByUserAsync(Guid userId)
        {
            return await _orderRepository
                .GetAllAttached()
                .Include(x => x.User)
                .Include(x => x.TiffanyLamp)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.OrderDate)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            return await _orderRepository
                .GetAllAttached()
                .Include(x => x.User)
                .Include(x => x.TiffanyLamp)
                .FirstOrDefaultAsync(x => x.Id == orderId);
        }

        public async Task<bool> RemoveByIdAsync(Guid orderId)
        {
            var order = await _orderRepository
                .GetAllAttached()
                .Include(x => x.TiffanyLamp)
                .FirstOrDefaultAsync(x => x.Id == orderId);

            if (order == null)
            {
                return false;
            }

            if (order.TiffanyLamp != null)
            {
                order.TiffanyLamp.Quantity += order.Quantity;
                await _tiffanyLampRepository.UpdateAsync(order.TiffanyLamp);
            }

            return await _orderRepository.DeleteAsync(order);
        }

        public async Task<bool> UpdateAsync(Guid orderId, Guid tiffanyLampId,
            Guid userId, int quantity)
        {
            var order = await _orderRepository
                .GetAllAttached()
                .Include(x => x.TiffanyLamp)
                .FirstOrDefaultAsync(x => x.Id == orderId);

            if (order == null || userId == Guid.Empty || quantity <= 0)
            {
                return false;
            }

            var newLamp = await _tiffanyLampRepository.GetByIdAsync(tiffanyLampId);
            if (newLamp == null)
            {
                return false;
            }

            if (order.TiffanyLampId == tiffanyLampId)
            {
                int quantityDifference = quantity - order.Quantity;

                if (quantityDifference > 0)
                {
                    if (newLamp.Quantity < quantityDifference)
                    {
                        return false;
                    }
                    newLamp.Quantity -= quantityDifference;
                }
                else if (quantityDifference < 0)
                {
                    newLamp.Quantity += Math.Abs(quantityDifference);
                }
            }
            else
            {
                var oldLamp = await _tiffanyLampRepository
                    .GetByIdAsync(order.TiffanyLampId);

                if (oldLamp != null)
                {
                    oldLamp.Quantity += order.Quantity;
                    await _tiffanyLampRepository.UpdateAsync(oldLamp);
                }

                if (newLamp.Quantity < quantity)
                {
                    return false;
                }

                newLamp.Quantity -= quantity;
            }

            order.TiffanyLampId = tiffanyLampId;
            order.UserId = userId;
            order.Quantity = quantity;
            order.Price = newLamp.Price;
            order.Discount = newLamp.Discount;
            order.OrderDate = DateTime.UtcNow;

            await _tiffanyLampRepository.UpdateAsync(newLamp);
            return await _orderRepository.UpdateAsync(order);
        }

        public async Task<bool> UserHasOrdersAsync(Guid userId)
        {
            return await _orderRepository
                .GetAllAttached()
                .AnyAsync(o => o.UserId == userId);
        }
    }
}