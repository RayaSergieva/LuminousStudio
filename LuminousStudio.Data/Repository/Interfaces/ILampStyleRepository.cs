namespace LuminousStudio.Data.Repository.Interfaces
{
    using Models;

    public interface ILampStyleRepository
        : IRepository<LampStyle, Guid>, IAsyncRepository<LampStyle, Guid>
    {
    }
}