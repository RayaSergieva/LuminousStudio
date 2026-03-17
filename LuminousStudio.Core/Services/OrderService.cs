using LuminousStudio.Core.Contracts;
using LuminousStudio.Infrastructure.Data;
using LuminousStudio.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuminousStudio.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITiffanyLampService _tiffanyLampService;

        public OrderService(ApplicationDbContext context, ITiffanyLampService tiffanyLampService)
        {
            _context = context;
            _tiffanyLampService = tiffanyLampService;
        }

        public bool Create(int tiffanyLampId, string userId, int quantity)
        {
            var tiffanyLamp = _context.TiffanyLamps.SingleOrDefault(x => x.Id == tiffanyLampId);

            if (tiffanyLamp == null || string.IsNullOrWhiteSpace(userId) || quantity <= 0 || tiffanyLamp.Quantity < quantity)
            {
                return false;
            }

            Order item = new Order
            {
                OrderDate = DateTime.Now,
                TiffanyLampId = tiffanyLampId,
                UserId = userId,
                Quantity = quantity,
                Price = tiffanyLamp.Price,
                Discount = tiffanyLamp.Discount
            };

            tiffanyLamp.Quantity -= quantity;

            _context.TiffanyLamps.Update(tiffanyLamp);
            _context.Orders.Add(item);

            return _context.SaveChanges() > 0;
        }

        public List<Order> GetOrders()
        {
            return _context.Orders
                .Include(x => x.User)
                .Include(x => x.TiffanyLamp)
                .OrderByDescending(x => x.OrderDate)
                .ToList();
        }

        public List<Order> GetOrdersByUser(string userId)
        {
            return _context.Orders
                .Include(x => x.User)
                .Include(x => x.TiffanyLamp)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.OrderDate)
                .ToList();
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders
                .Include(x => x.User)
                .Include(x => x.TiffanyLamp)
                .FirstOrDefault(x => x.Id == orderId);
        }

        public bool RemoveById(int orderId)
        {
            var order = _context.Orders
                .Include(x => x.TiffanyLamp)
                .FirstOrDefault(x => x.Id == orderId);

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

            return _context.SaveChanges() > 0;
        }

        public bool Update(int orderId, int tiffanyLampId, string userId, int quantity)
        {
            var order = _context.Orders
                .Include(x => x.TiffanyLamp)
                .FirstOrDefault(x => x.Id == orderId);

            if (order == null || string.IsNullOrWhiteSpace(userId) || quantity <= 0)
            {
                return false;
            }

            var newLamp = _context.TiffanyLamps.FirstOrDefault(x => x.Id == tiffanyLampId);
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
                var oldLamp = _context.TiffanyLamps.FirstOrDefault(x => x.Id == order.TiffanyLampId);
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

            return _context.SaveChanges() > 0;
        }
    }
}