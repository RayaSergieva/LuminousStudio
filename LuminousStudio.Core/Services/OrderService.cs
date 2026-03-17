using LuminousStudio.Core.Contracts;
using LuminousStudio.Infrastructure.Data;
using LuminousStudio.Infrastructure.Data.Entities;

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

            if (tiffanyLamp == null)
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

            this._context.TiffanyLamps.Update(tiffanyLamp);
            this._context.Orders.Add(item);

            return _context.SaveChanges() != 0;
        }

        public Order GetOrderById(int orderId)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrders()
        {
            return _context.Orders.OrderByDescending(x => x.OrderDate).ToList();
        }

        public List<Order> GetOrdersByUser(string userId)
        {
            return _context.Orders.Where(x => x.UserId == userId)
                .OrderByDescending(x => x.OrderDate)
                .ToList();
        }

        public bool RemoveById(int orderId)
        {
            throw new NotImplementedException();
        }

        public bool Update(int orderId, int tiffanyLampId, string userId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
