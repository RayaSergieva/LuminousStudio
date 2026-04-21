namespace LuminousStudio.Services.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;

    using LuminousStudio.Data.Models;
    using LuminousStudio.Data.Repository.Interfaces;
    using LuminousStudio.Services.Core.Contracts;
    using LuminousStudio.Services.Common;

    public class StatisticService : IStatisticService
    {
        private readonly ITiffanyLampRepository _tiffanyLampRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public StatisticService(
            ITiffanyLampRepository tiffanyLampRepository,
            IOrderRepository orderRepository,
            UserManager<ApplicationUser> userManager)
        {
            _tiffanyLampRepository = tiffanyLampRepository;
            _orderRepository = orderRepository;
            _userManager = userManager;
        }

        public async Task<int> CountTiffanyLampsAsync()
        {
            return await _tiffanyLampRepository.CountAsync();
        }

        public async Task<int> CountOrdersAsync()
        {
            return await _orderRepository.CountAsync();
        }

        public async Task<int> CountClientsAsync()
        {
            var adminUsers = await _userManager.GetUsersInRoleAsync(
                ApplicationRoles.Administrator);

            var adminIds = adminUsers.Select(u => u.Id).ToList();

            return await _userManager.Users
                .CountAsync(u => !adminIds.Contains(u.Id));
        }

        public async Task<decimal> SumOrdersAsync()
        {
            return await _orderRepository
                .GetAllAttached()
                .SumAsync(x => x.Quantity * x.Price - x.Quantity * x.Price * x.Discount / 100);
        }
    }
}