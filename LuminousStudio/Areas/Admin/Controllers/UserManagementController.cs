namespace LuminousStudio.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using LuminousStudio.Services.Admin.Contracts;
    using LuminousStudio.Web.ViewModels.Admin.UserManagement;

    public class UserManagementController : BaseAdminController
    {
        private readonly IUserManagementService _userManagementService;

        public UserManagementController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var clients = (await _userManagementService.GetAllClientsAsync())
                .Select(u => new UserManagementIndexVM
                {
                    Id = u.Id,
                    UserName = u.UserName ?? "Unknown",
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Address = u.Address,
                    Email = u.Email ?? "Unknown"
                }).ToList();

            return View(clients);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManagementService.GetClientByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserDeleteVM
            {
                Id = user.Id,
                UserName = user.UserName ?? "Unknown",
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email ?? "Unknown",
                Address = user.Address
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(UserDeleteVM model)
        {
            bool hasOrders = await _userManagementService.ClientHasOrdersAsync(model.Id);
            if (hasOrders)
            {
                ModelState.AddModelError(string.Empty,
                    "The client has orders and cannot be deleted.");
                return View(model);
            }

            var deleted = await _userManagementService.DeleteClientAsync(model.Id);
            if (deleted)
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}