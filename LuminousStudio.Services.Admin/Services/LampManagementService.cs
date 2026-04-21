namespace LuminousStudio.Services.Admin.Services
{
    using Microsoft.EntityFrameworkCore;

    using LuminousStudio.Data.Models;
    using LuminousStudio.Data.Repository.Interfaces;
    using LuminousStudio.Services.Admin.Contracts;

    public class LampManagementService : ILampManagementService
    {
        private readonly ITiffanyLampRepository _tiffanyLampRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ILampStyleRepository _lampStyleRepository;

        public LampManagementService(
            ITiffanyLampRepository tiffanyLampRepository,
            IManufacturerRepository manufacturerRepository,
            ILampStyleRepository lampStyleRepository)
        {
            _tiffanyLampRepository = tiffanyLampRepository;
            _manufacturerRepository = manufacturerRepository;
            _lampStyleRepository = lampStyleRepository;
        }

        public async Task<IEnumerable<TiffanyLamp>> GetAllLampsAsync()
        {
            return await _tiffanyLampRepository
                .GetAllAttached()
                .Include(x => x.Manufacturer)
                .Include(x => x.LampStyle)
                .ToListAsync();
        }

        public async Task<TiffanyLamp?> GetLampByIdAsync(Guid id)
        {
            return await _tiffanyLampRepository
                .GetAllAttached()
                .Include(x => x.Manufacturer)
                .Include(x => x.LampStyle)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> CreateLampAsync(string name, Guid manufacturerId,
            Guid lampStyleId, string picture, int quantity, decimal price, decimal discount)
        {
            TiffanyLamp lamp = new TiffanyLamp
            {
                TiffanyLampName = name,
                ManufacturerId = manufacturerId,
                LampStyleId = lampStyleId,
                Picture = picture,
                Quantity = quantity,
                Price = price,
                Discount = discount
            };

            await _tiffanyLampRepository.AddAsync(lamp);
            return true;
        }

        public async Task<bool> UpdateLampAsync(Guid id, string name, Guid manufacturerId,
            Guid lampStyleId, string picture, int quantity, decimal price, decimal discount)
        {
            var lamp = await GetLampByIdAsync(id);
            if (lamp == null)
            {
                return false;
            }

            lamp.TiffanyLampName = name;
            lamp.ManufacturerId = manufacturerId;
            lamp.LampStyleId = lampStyleId;
            lamp.Picture = picture;
            lamp.Quantity = quantity;
            lamp.Price = price;
            lamp.Discount = discount;

            return await _tiffanyLampRepository.UpdateAsync(lamp);
        }

        public async Task<bool> DeleteLampAsync(Guid id)
        {
            var lamp = await GetLampByIdAsync(id);
            if (lamp == null)
            {
                return false;
            }

            return await _tiffanyLampRepository.DeleteAsync(lamp);
        }
    }
}