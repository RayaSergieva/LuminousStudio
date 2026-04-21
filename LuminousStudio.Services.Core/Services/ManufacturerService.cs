namespace LuminousStudio.Services.Core.Services
{
    using Microsoft.EntityFrameworkCore;

    using LuminousStudio.Data.Models;
    using LuminousStudio.Data.Repository.Interfaces;
    using LuminousStudio.Services.Core.Contracts;

    public class ManufacturerService : IManufacturerService
    {
        private readonly IManufacturerRepository _manufacturerRepository;

        public ManufacturerService(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        public async Task<Manufacturer?> GetManufacturerByIdAsync(Guid manufacturerId)
        {
            return await _manufacturerRepository.GetByIdAsync(manufacturerId);
        }

        public async Task<List<Manufacturer>> GetManufacturersAsync()
        {
            return (await _manufacturerRepository.GetAllAsync()).ToList();
        }

        public async Task<List<TiffanyLamp>> GetProductsByManufacturerAsync(Guid manufacturerId)
        {
            return await _manufacturerRepository
                .GetAllAttached()
                .Where(m => m.Id == manufacturerId)
                .SelectMany(m => m.TiffanyLamps)
                .ToListAsync();
        }
    }
}