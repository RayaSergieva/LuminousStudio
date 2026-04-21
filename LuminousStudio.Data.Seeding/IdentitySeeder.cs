namespace LuminousStudio.Data.Seeding
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;

    using LuminousStudio.Data.Models;
    using LuminousStudio.Data.Seeding.Interfaces;
    using LuminousStudio.Services.Common;
    using LuminousStudio.Web.Common.Configuration;

    public class IdentitySeeder : IIdentitySeeder
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AdminSettings _adminSettings;

        public IdentitySeeder(
            RoleManager<IdentityRole<Guid>> roleManager,
            UserManager<ApplicationUser> userManager,
            IOptions<AdminSettings> adminSettings)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _adminSettings = adminSettings.Value;
        }

        public async Task SeedIdentityAsync()
        {
            await SeedRolesAsync();
            await SeedAdminAsync();
        }

        private async Task SeedRolesAsync()
        {
            string[] roles = { ApplicationRoles.Administrator, ApplicationRoles.Client };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole<Guid> { Name = role });
                }
            }
        }

        private async Task SeedAdminAsync()
        {
            if (await _userManager.FindByNameAsync(_adminSettings.Username) != null)
            {
                return;
            }

            ApplicationUser admin = new ApplicationUser
            {
                FirstName = _adminSettings.FirstName,
                LastName = _adminSettings.LastName,
                UserName = _adminSettings.Username,
                Email = _adminSettings.Email,
                Address = _adminSettings.Address,
                PhoneNumber = _adminSettings.Phone
            };

            var result = await _userManager.CreateAsync(admin, _adminSettings.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(admin, ApplicationRoles.Administrator);
            }
        }
    }
}