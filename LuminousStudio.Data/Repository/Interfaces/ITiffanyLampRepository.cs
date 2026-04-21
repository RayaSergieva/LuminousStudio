namespace LuminousStudio.Data.Repository.Interfaces
{
    using Models;

    public interface ITiffanyLampRepository
        : IRepository<TiffanyLamp, Guid>, IAsyncRepository<TiffanyLamp, Guid>
    {
    }
}