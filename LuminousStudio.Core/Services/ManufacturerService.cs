using LuminousStudio.Core.Contracts;
using LuminousStudio.Infrastructure.Data;
using LuminousStudio.Infrastructure.Data.Entities;

namespace LuminousStudio.Core.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly ApplicationDbContext _context;

        public ManufacturerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Manufacturer GetManufacturerById(int manufacturerId)
        {
            return _context.Manufacturers.Find(manufacturerId);
        }

        public List<Manufacturer> GetManufacturers()
        {
            List<Manufacturer> manufacturers = _context.Manufacturers.ToList();
            return manufacturers;
        }

        public List<TiffanyLamp> GetProductsByManufacturer(int manufacturerId)
        {
            return _context.TiffanyLamps
                .Where(x => x.ManufacturerId == manufacturerId)
                .ToList();
        }
    }
}
