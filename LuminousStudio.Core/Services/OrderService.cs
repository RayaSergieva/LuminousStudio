namespace LuminousStudio.Core.Services
{
    using Microsoft.EntityFrameworkCore;

    using LuminousStudio.Core.Contracts;
    using LuminousStudio.Infrastructure.Data;
    using LuminousStudio.Infrastructure.Data.Entities;

    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Guid tiffanyLampId, Guid userId, int quantity)
        {
            var tiffanyLamp = await _context.TiffanyLamps.SingleOrDefaultAsync(x => x.Id == tiffanyLampId);

            if (tiffanyLamp == null || userId == Guid.Empty || quantity <= 0 || tiffanyLamp.Quantity < quantity)
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

            _context.TiffanyLamps.Update(tiffanyLamp);
            _context.Orders.Add(item);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _context.Orders
                .Include(x => x.User)
                .Include(x => x.TiffanyLamp)
                .OrderByDescending(x => x.OrderDate)
                .ToListAsync();
        }

        public async Task<List<Order>> GetOrdersByUserAsync(Guid userId)
        {
            return await _context.Orders
                .Include(x => x.User)
                .Include(x => x.TiffanyLamp)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.OrderDate)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            return await _context.Orders
                .Include(x => x.User)
                .Include(x => x.TiffanyLamp)
                .FirstOrDefaultAsync(x => x.Id == orderId);
        }

        public async Task<bool> RemoveByIdAsync(Guid orderId)
        {
            var order = await _context.Orders
                .Include(x => x.TiffanyLamp)
                .FirstOrDefaultAsync(x => x.Id == orderId);

            if (order == null)
            {
                return false;
            }

            if (order.TiffanyLamp != null)
            {
                order.TiffanyLamp.Quantity += order.Quantity;
                _context.TiffanyLamps.Update(order.TiffanyLamp);
            }

            _context.Orders.Remove(order);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Guid orderId, Guid tiffanyLampId, Guid userId, int quantity)
        {
            var order = await _context.Orders
                .Include(x => x.TiffanyLamp)
                .FirstOrDefaultAsync(x => x.Id == orderId);

            if (order == null || userId == Guid.Empty || quantity <= 0)
            {
                return false;
            }

            var newLamp = await _context.TiffanyLamps.FirstOrDefaultAsync(x => x.Id == tiffanyLampId);
            
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
                var oldLamp = await _context.TiffanyLamps.FirstOrDefaultAsync(x => x.Id == order.TiffanyLampId);
                if (oldLamp != null)
                {
                    oldLamp.Quantity += order.Quantity;
                    _context.TiffanyLamps.Update(oldLamp);
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
            order.OrderDate = DateTime.Now;

            _context.TiffanyLamps.Update(newLamp);
            _context.Orders.Update(order);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UserHasOrdersAsync(Guid userId)
        {
            return await _context.Orders.AnyAsync(o => o.UserId == userId);
        }
    }
}