using LuminousStudio.Core.Contracts;
using LuminousStudio.Infrastructure.Data;
using LuminousStudio.Infrastructure.Data.Entities;

namespace LuminousStudio.Core.Services
{
    public class TiffanyLampService : ITiffanyLampService
    {
        private readonly ApplicationDbContext _context;

        public TiffanyLampService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(string name, int manufacturerId, int lampStyleId, string picture, int quantity, decimal price, decimal discount)
        {
            TiffanyLamp item = new TiffanyLamp
            {
                TiffanyLampName = name,
                Manufacturer = _context.Manufacturers.Find(manufacturerId),
                LampStyle = _context.LampStyles.Find(lampStyleId),
                Picture = picture,
                Quantity = quantity,
                Price = price,
                Discount = discount
            };

            _context.TiffanyLamps.Add(item);
            return _context.SaveChanges() != 0;
        }

        public TiffanyLamp GetTiffanyLampById(int tiffanyLampId)
        {
            return _context.TiffanyLamps.Find(tiffanyLampId);
        }

        public List<TiffanyLamp> GetTiffanyLamps()
        {
            List<TiffanyLamp> tiffanyLamps = _context.TiffanyLamps.ToList();
            return tiffanyLamps;
        }

        public List<TiffanyLamp> GetTiffanyLamps(string searchStringLampStyleName, string searchStringManufacturerName)
        {
            List<TiffanyLamp> tiffanyLamps = _context.TiffanyLamps.ToList();

            if (!String.IsNullOrEmpty(searchStringLampStyleName) && !String.IsNullOrEmpty(searchStringManufacturerName))
            {
                tiffanyLamps = tiffanyLamps.Where(x => x.LampStyle.LampStyleName.ToLower().Contains(searchStringLampStyleName.ToLower())
                                            && x.Manufacturer.ManufacturerName.ToLower().Contains(searchStringManufacturerName.ToLower())).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringLampStyleName))
            {
                tiffanyLamps = tiffanyLamps.Where(x => x.LampStyle.LampStyleName.ToLower().Contains(searchStringLampStyleName.ToLower())).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringManufacturerName))
            {
                tiffanyLamps = tiffanyLamps.Where(x => x.Manufacturer.ManufacturerName.ToLower().Contains(searchStringManufacturerName.ToLower())).ToList();
            }

            return tiffanyLamps;
        }

        public bool RemoveById(int tiffanyLampId)
        {
            var tiffanyLamp = GetTiffanyLampById(tiffanyLampId);
            if (tiffanyLamp == default(TiffanyLamp))
            {
                return false;
            }
            _context.Remove(tiffanyLamp);
            return _context.SaveChanges() != 0;
        }

        public bool Update(int tiffanyLampId, string name, int manufacturerId, int lampStyleId, string picture, int quantity, decimal price, decimal discount)
        {
            var tiffanyLamp = GetTiffanyLampById(tiffanyLampId);
            if (tiffanyLamp == default(TiffanyLamp))
            {
                return false;
            }

            tiffanyLamp.TiffanyLampName = name;
            tiffanyLamp.Manufacturer = _context.Manufacturers.Find(manufacturerId);
            tiffanyLamp.LampStyle = _context.LampStyles.Find(lampStyleId);
            tiffanyLamp.Picture = picture;
            tiffanyLamp.Quantity = quantity;
            tiffanyLamp.Price = price;
            tiffanyLamp.Discount = discount;

            _context.Update(tiffanyLamp);
            return _context.SaveChanges() != 0;
        }
    }
}
