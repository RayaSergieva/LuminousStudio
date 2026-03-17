using LuminousStudio.Infrastructure.Data;
using LuminousStudio.Infrastructure.Data.Entities;
using LuminousStudio.Models.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LuminousStudio.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ClientController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ClientController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            this._userManager = userManager;
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allUsers = this._userManager.Users
                .Select(u => new ClientIndexVM
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Address = u.Address,
                    Email = u.Email
                })
                .ToList();

            var adminIds = (await _userManager.GetUsersInRoleAsync("Administrator"))
                .Select(a => a.Id).ToList();

            foreach (var user in allUsers)
            {
                user.IsAdmin = adminIds.Contains(user.Id);
            }

            var users = allUsers.Where(x => x.IsAdmin == false)
                .OrderBy(x => x.UserName).ToList();

            return this.View(users);
        }

        public ActionResult Delete(string id)
        {
            var user = this._userManager.Users.FirstOrDefault(x => x.Id == id);

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
                Email = user.Email,
                UserName = user.UserName
            };

            return View(userToDelete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ClientDeleteVM bidingModel)
        {
            string id = bidingModel.Id;
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            bool hasOrders = _context.Orders.Any(o => o.UserId == user.Id);
            if (hasOrders)
            {
                var model = new ClientDeleteVM
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address,
                    Email = user.Email,
                    UserName = user.UserName
                };

                ModelState.Clear();

                ModelState.AddModelError(string.Empty, "The client has orders and cannot be deleted.");

                return View(model);
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Success");
            }

            return NotFound();
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
