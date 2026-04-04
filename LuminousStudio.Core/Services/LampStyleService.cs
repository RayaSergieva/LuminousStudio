namespace LuminousStudio.Core.Services
{
    using Microsoft.EntityFrameworkCore;

    using LuminousStudio.Core.Contracts;
    using LuminousStudio.Infrastructure.Data;
    using LuminousStudio.Infrastructure.Data.Entities;

    public class LampStyleService : ILampStyleService
    {
        private readonly ApplicationDbContext _context;

        public LampStyleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LampStyle?> GetLampStyleByIdAsync(Guid lampStyleId)
        {
            return await _context.LampStyles.FindAsync(lampStyleId);
        }

        public async Task<List<LampStyle>> GetLampStylesAsync()
        {
            return await _context.LampStyles.ToListAsync();
        }

        public async Task<List<TiffanyLamp>> GetProductsByLampStyleAsync(Guid lampStyleId)
        {
            return await _context.TiffanyLamps
                .Where(x => x.LampStyleId == lampStyleId)
                .ToListAsync();
        }
    }
}
