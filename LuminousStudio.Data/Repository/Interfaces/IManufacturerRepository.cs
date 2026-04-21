namespace LuminousStudio.Data.Repository.Interfaces
{
    using Models;

    public interface IManufacturerRepository
        : IRepository<Manufacturer, Guid>, IAsyncRepository<Manufacturer, Guid>
    {
    }
}