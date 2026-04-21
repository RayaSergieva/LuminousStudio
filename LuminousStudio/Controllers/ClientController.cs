namespace LuminousStudio.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using LuminousStudio.Data.Models;
    using LuminousStudio.Services.Core.Contracts;
    using LuminousStudio.Services.Common;
    using LuminousStudio.Web.ViewModels.Client;

    [Authorize(Roles = ApplicationRoles.Administrator)]
    public class ClientController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderService _orderService;

        public ClientController(UserManager<ApplicationUser> userManager, IOrderService orderService)
        {
            _userManager = userManager;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allUsers = _userManager.Users
                .Select(u => new ClientIndexVM
                {
                    Id = u.Id,
                    UserName = u.UserName ?? "Unknown",
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Address = u.Address,
                    Email = u.Email ?? "Unknown"
                })
                .ToList();

            var adminIds = (await _userManager.GetUsersInRoleAsync("Administrator"))
                .Select(a => a.Id).ToList();

            foreach (var user in allUsers)
            {
                user.IsAdmin = adminIds.Contains(user.Id);
            }

            var users = allUsers.Where(x => !x.IsAdmin)
                .OrderBy(x => x.UserName).ToList();

            return View(users);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            ClientDeleteVM userToDelete = new ClientDeleteVM()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Email = user.Email ?? "Unknown",
                UserName = user.UserName ?? "Unknown"
            };

            return View(userToDelete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ClientDeleteVM bidingModel)
        {
            Guid id = bidingModel.Id;
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound();
            }

            bool hasOrders = await _orderService.UserHasOrdersAsync(user.Id);
            if (hasOrders)
            {
                var model = new ClientDeleteVM
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address,
                    Email = user.Email ?? "Unknown",
                    UserName = user.UserName ?? "Unknown"
                };

                ModelState.Clear();

                ModelState.AddModelError(string.Empty, "The client has orders and cannot be deleted.");

                return View(model);
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Success));
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }
    }
}
