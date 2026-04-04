namespace LuminousStudio.Core.Services
{
    using Microsoft.EntityFrameworkCore;

    using LuminousStudio.Core.Contracts;
    using LuminousStudio.Infrastructure.Data;
    using LuminousStudio.Infrastructure.Data.Entities;

    public class TiffanyLampService : ITiffanyLampService
    {
        private readonly ApplicationDbContext _context;

        public TiffanyLampService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(string name, Guid manufacturerId, Guid lampStyleId, string picture, int quantity, decimal price, decimal discount)
        {
            TiffanyLamp item = new TiffanyLamp
            {
                TiffanyLampName = name,
                ManufacturerId = manufacturerId,
                LampStyleId = lampStyleId,
                Picture = picture,
                Quantity = quantity,
                Price = price,
                Discount = discount
            };

            _context.TiffanyLamps.Add(item);
            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<TiffanyLamp?> GetTiffanyLampByIdAsync(Guid tiffanyLampId)
        {
            return await _context.TiffanyLamps
                .Include(x => x.Manufacturer)
                .Include(x => x.LampStyle)
                .FirstOrDefaultAsync(x => x.Id == tiffanyLampId);
        }

        public async Task<List<TiffanyLamp>> GetTiffanyLampsAsync()
        {
            return await _context.TiffanyLamps
                .Include(x => x.Manufacturer)
                .Include(x => x.LampStyle)
                .ToListAsync();
        }

        public async Task<List<TiffanyLamp>> GetTiffanyLampsAsync(string searchStringLampStyleName, string searchStringManufacturerName)
        {
            IQueryable<TiffanyLamp> query = _context.TiffanyLamps
                .Include(x => x.Manufacturer)
                .Include(x => x.LampStyle);

            if (!string.IsNullOrEmpty(searchStringLampStyleName) && !string.IsNullOrEmpty(searchStringManufacturerName))
            {
                query = query.Where(x =>
                    x.LampStyle.LampStyleName.Contains(searchStringLampStyleName) &&
                    x.Manufacturer.ManufacturerName.Contains(searchStringManufacturerName));
            }
            else if (!string.IsNullOrEmpty(searchStringLampStyleName))
            {
                query = query.Where(x =>
                    x.LampStyle.LampStyleName.Contains(searchStringLampStyleName));
            }
            else if (!string.IsNullOrEmpty(searchStringManufacturerName))
            {
                query = query.Where(x =>
                    x.Manufacturer.ManufacturerName.Contains(searchStringManufacturerName));
            }

            return await query.ToListAsync();
        }

        public async Task<bool> RemoveByIdAsync(Guid tiffanyLampId)
        {
            var tiffanyLamp = await GetTiffanyLampByIdAsync(tiffanyLampId);
            if (tiffanyLamp == null)
            {
                return false;
            }
            _context.Remove(tiffanyLamp);
            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<bool> UpdateAsync(Guid tiffanyLampId, string name, Guid manufacturerId, Guid lampStyleId, string picture, int quantity, decimal price, decimal discount)
        {
            var tiffanyLamp = await GetTiffanyLampByIdAsync(tiffanyLampId);
            if (tiffanyLamp == null)
            {
                return false;
            }

            tiffanyLamp.TiffanyLampName = name;
            tiffanyLamp.ManufacturerId = manufacturerId;
            tiffanyLamp.LampStyleId = lampStyleId;
            tiffanyLamp.Picture = picture;
            tiffanyLamp.Quantity = quantity;
            tiffanyLamp.Price = price;
            tiffanyLamp.Discount = discount;

            _context.Update(tiffanyLamp);
            return await _context.SaveChangesAsync() != 0;
        }
    }
}
