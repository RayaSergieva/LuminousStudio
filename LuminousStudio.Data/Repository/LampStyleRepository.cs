namespace LuminousStudio.Data.Repository
{
    using Interfaces;
    using Models;

    public class LampStyleRepository
        : BaseRepository<LampStyle, Guid>, ILampStyleRepository
    {
        public LampStyleRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}