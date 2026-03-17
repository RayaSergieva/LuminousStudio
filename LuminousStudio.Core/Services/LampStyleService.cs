using LuminousStudio.Core.Contracts;
using LuminousStudio.Infrastructure.Data;
using LuminousStudio.Infrastructure.Data.Entities;

namespace LuminousStudio.Core.Services
{
    public class LampStyleService : ILampStyleService
    {
        private readonly ApplicationDbContext _context;

        public LampStyleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public LampStyle GetLampStyleById(int lampStyleId)
        {
            return _context.LampStyles.Find(lampStyleId);
        }

        public List<LampStyle> GetLampStyles()
        {
            List<LampStyle> lampStyles = _context.LampStyles.ToList();
            return lampStyles;
        }

        public List<TiffanyLamp> GetProductsByLampStyle(int lampStyleId)
        {
            return _context.TiffanyLamps
                .Where(x => x.LampStyleId == lampStyleId)
                .ToList();
        }
    }
}
