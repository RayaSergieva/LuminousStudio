namespace LuminousStudio.Data.Repository
{
    using Interfaces;
    using Models;

    public class ManufacturerRepository
        : BaseRepository<Manufacturer, Guid>, IManufacturerRepository
    {
        public ManufacturerRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}