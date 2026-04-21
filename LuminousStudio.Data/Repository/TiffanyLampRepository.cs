namespace LuminousStudio.Data.Repository
{
    using Interfaces;
    using Models;

    public class TiffanyLampRepository
        : BaseRepository<TiffanyLamp, Guid>, ITiffanyLampRepository
    {
        public TiffanyLampRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}