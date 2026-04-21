namespace LuminousStudio.Services.Admin.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using LuminousStudio.Data.Models;
    using LuminousStudio.Data.Repository.Interfaces;
    using LuminousStudio.Services.Admin.Contracts;
    using LuminousStudio.Services.Common;

    public class UserManagementService : IUserManagementService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderRepository _orderRepository;

        public UserManagementService(
            UserManager<ApplicationUser> userManager,
            IOrderRepository orderRepository)
        {
            _userManager = userManager;
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllClientsAsync()
        {
            var adminUsers = await _userManager.GetUsersInRoleAsync(
                ApplicationRoles.Administrator);
            var adminIds = adminUsers.Select(u => u.Id).ToList();

            return await _userManager.Users
                .Where(u => !adminIds.Contains(u.Id))
                .OrderBy(u => u.UserName)
                .ToListAsync();
        }

        public async Task<ApplicationUser?> GetClientByIdAsync(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<bool> ClientHasOrdersAsync(Guid id)
        {
            return await _orderRepository
                .GetAllAttached()
                .AnyAsync(o => o.UserId == id);
        }

        public async Task<bool> DeleteClientAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return false;
            }

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
    }
}