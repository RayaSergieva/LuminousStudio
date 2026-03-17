using LuminousStudio.Core.Contracts;
using LuminousStudio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LuminousStudio.Core.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly ApplicationDbContext _context;

        public StatisticService(ApplicationDbContext context)
        {
            _context = context;
        }

        public int CountClients()
        {
            var adminUserIds = _context.UserRoles
                .Join(
                    _context.Roles,
                    userRole => userRole.RoleId,
                    role => role.Id,
                    (userRole, role) => new { userRole.UserId, role.Name })
                .Where(x => x.Name == "Administrator")
                .Select(x => x.UserId)
                .Distinct()
                .ToList();

            return _context.Users.Count(u => !adminUserIds.Contains(u.Id));
        }

        public int CountOrders()
        {
            return _context.Orders.Count();
        }

        public int CountTiffanyLamps()
        {
            return _context.TiffanyLamps.Count();
        }

        public decimal SumOrders()
        {
            return _context.Orders
                .Sum(x => x.Quantity * x.Price - x.Quantity * x.Price * x.Discount / 100);
        }
    }
}