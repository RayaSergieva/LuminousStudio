namespace LuminousStudio.Core.Services
{
    using Microsoft.EntityFrameworkCore;

    using LuminousStudio.Core.Contracts;
    using LuminousStudio.Infrastructure.Data;
    public class StatisticService : IStatisticService
    {
        private readonly ApplicationDbContext _context;

        public StatisticService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountClientsAsync()
        {
            var adminUserIds = await _context.UserRoles
                .Join(
                    _context.Roles,
                    userRole => userRole.RoleId,
                    role => role.Id,
                    (userRole, role) => new { userRole.UserId, role.Name })
                .Where(x => x.Name == "Administrator")
                .Select(x => x.UserId)
                .Distinct()
                .ToListAsync();

            return await _context.Users
                .CountAsync(u => !adminUserIds.Contains(u.Id));
        }

        public async Task<int> CountOrdersAsync()
        {
            return await _context.Orders.CountAsync();
        }

        public async Task<int> CountTiffanyLampsAsync()
        {
            return await _context.TiffanyLamps.CountAsync();
        }

        public async Task<decimal> SumOrdersAsync()
        {
            return await _context.Orders
                .SumAsync(x => x.Quantity * x.Price - x.Quantity * x.Price * x.Discount / 100);
        }
    }
}