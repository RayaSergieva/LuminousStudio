namespace LuminousStudio.Data.Seeding
{
    using LuminousStudio.Data.Models;
    using LuminousStudio.Data.Seeding.Interfaces;

    public class LampStyleSeeder : ISeeder
    {
        private readonly ApplicationDbContext _context;

        public LampStyleSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (_context.LampStyles.Any())
            {
                return;
            }

            var lampStyles = new[]
            {
                new LampStyle { LampStyleName = "Table Lamp" },
                new LampStyle { LampStyleName = "Floor Lamp" },
                new LampStyle { LampStyleName = "Chandelier" },
                new LampStyle { LampStyleName = "Wall Sconce" },
                new LampStyle { LampStyleName = "Pendant Lamp" },
                new LampStyle { LampStyleName = "Ceiling Lamp" },
                new LampStyle { LampStyleName = "Banker's Lamp" }
            };

            await _context.LampStyles.AddRangeAsync(lampStyles);
            await _context.SaveChangesAsync();
        }
    }
}