namespace LuminousStudio.Data.Seeding
{
    using LuminousStudio.Data.Models;
    using LuminousStudio.Data.Seeding.Interfaces;

    public class ManufacturerSeeder : ISeeder
    {
        private readonly ApplicationDbContext _context;

        public ManufacturerSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (_context.Manufacturers.Any())
            {
                return;
            }

            var manufacturers = new[]
            {
                new Manufacturer { ManufacturerName = "Louis Comfort Tiffany" },
                new Manufacturer { ManufacturerName = "Clara Driscoll" },
                new Manufacturer { ManufacturerName = "Edward Sperry" },
                new Manufacturer { ManufacturerName = "Frederick Wilson" },
                new Manufacturer { ManufacturerName = "Jacob Holzer" },
                new Manufacturer { ManufacturerName = "Agnes F. Northrop" },
                new Manufacturer { ManufacturerName = "Joseph Lauber" },
                new Manufacturer { ManufacturerName = "Maxfield Parrish" }
            };

            await _context.Manufacturers.AddRangeAsync(manufacturers);
            await _context.SaveChangesAsync();
        }
    }
}