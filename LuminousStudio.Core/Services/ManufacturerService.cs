namespace LuminousStudio.Core.Services
{
    using Microsoft.EntityFrameworkCore;

    using LuminousStudio.Core.Contracts;
    using LuminousStudio.Infrastructure.Data;
    using LuminousStudio.Infrastructure.Data.Entities;

    public class ManufacturerService : IManufacturerService
    {
        private readonly ApplicationDbContext _context;

        public ManufacturerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Manufacturer?> GetManufacturerByIdAsync(Guid manufacturerId)
        {
            return await _context.Manufacturers.FindAsync(manufacturerId);
        }

        public async Task<List<Manufacturer>> GetManufacturersAsync()
        {
            return await _context.Manufacturers.ToListAsync();
        }

        public async Task<List<TiffanyLamp>> GetProductsByManufacturerAsync(Guid manufacturerId)
        {
            return await _context.TiffanyLamps
                .Where(x => x.ManufacturerId == manufacturerId)
                .ToListAsync();
        }
    }
}
