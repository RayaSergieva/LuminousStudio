using LuminousStudio.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LuminousStudio.Infrastructure.Data.Infrastructure
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            await RoleSeeder(services);
            await SeedAdministrator(services);

            var dataLampStyle = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedLampStyles(dataLampStyle);

            var dataManufacturer = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedManufacturers(dataManufacturer);

            return app;
        }

        private static async Task RoleSeeder(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Administrator", "Client" };
            IdentityResult roleResult;

            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);

                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private static async Task SeedAdministrator(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (await userManager.FindByNameAsync("admin") == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    FirstName = "admin",
                    LastName = "admin",
                    UserName = "admin",
                    Email = "admin@admin.com",
                    Address = "admin address",
                    PhoneNumber = "0888888888"
                };

                var result = await userManager.CreateAsync(user, "Admin123456");

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }

        private static void SeedLampStyles(ApplicationDbContext dataLampStyle)
        {
            if (dataLampStyle.LampStyles.Any())
            {
                return;
            }

            dataLampStyle.LampStyles.AddRange(new[]
            {
                new LampStyle  { LampStyleName = "Table Lamp" },
                new LampStyle  { LampStyleName = "Floor Lamp" },
                new LampStyle  { LampStyleName = "Chandelier" },
                new LampStyle  { LampStyleName = "Wall Sconce" },
                new LampStyle  { LampStyleName = "Pendant Lamp" },
                new LampStyle  { LampStyleName = "Ceiling Lamp" },
                new LampStyle  { LampStyleName = "Banker’s Lamp" }
            });

            dataLampStyle.SaveChanges();
        }

        private static void SeedManufacturers(ApplicationDbContext dataManufacturer)
        {
            if (dataManufacturer.Manufacturers.Any())
            {
                return;
            }

            dataManufacturer.Manufacturers.AddRange(new[]
            {
                new Manufacturer  { ManufacturerName = "Louis Comfort Tiffany" },
                new Manufacturer  { ManufacturerName = "Clara Driscoll" },
                new Manufacturer  { ManufacturerName = "Edward Sperry" },
                new Manufacturer  { ManufacturerName = "Frederick Wilson" },
                new Manufacturer  { ManufacturerName = "Jacob Holzer" },
                new Manufacturer  { ManufacturerName = "Agnes F. Northrop" },
                new Manufacturer  { ManufacturerName = "Joseph Lauber" },
                new Manufacturer  { ManufacturerName = "Maxfield Parrish" }
            });

            dataManufacturer.SaveChanges();
        }
    }
}
