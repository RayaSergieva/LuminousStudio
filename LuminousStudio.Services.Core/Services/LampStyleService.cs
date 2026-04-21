namespace LuminousStudio.Services.Core.Services
{
    using Microsoft.EntityFrameworkCore;

    using LuminousStudio.Data.Models;
    using LuminousStudio.Data.Repository.Interfaces;
    using LuminousStudio.Services.Core.Contracts;

    public class LampStyleService : ILampStyleService
    {
        private readonly ILampStyleRepository _lampStyleRepository;

        public LampStyleService(ILampStyleRepository lampStyleRepository)
        {
            _lampStyleRepository = lampStyleRepository;
        }

        public async Task<LampStyle?> GetLampStyleByIdAsync(Guid lampStyleId)
        {
            return await _lampStyleRepository.GetByIdAsync(lampStyleId);
        }

        public async Task<List<LampStyle>> GetLampStylesAsync()
        {
            return (await _lampStyleRepository.GetAllAsync()).ToList();
        }

        public async Task<List<TiffanyLamp>> GetProductsByLampStyleAsync(Guid lampStyleId)
        {
            return await _lampStyleRepository
                .GetAllAttached()
                .Where(ls => ls.Id == lampStyleId)
                .SelectMany(ls => ls.TiffanyLamps)
                .ToListAsync();
        }
    }
}