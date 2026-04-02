namespace LuminousStudio.Core.Contracts
{
    using LuminousStudio.Infrastructure.Data.Entities;

    public interface ILampStyleService
    {
        Task<List<LampStyle>> GetLampStylesAsync();
        Task<LampStyle?> GetLampStyleByIdAsync(Guid lampStyleId);
        Task<List<TiffanyLamp>> GetProductsByLampStyleAsync(Guid lampStyleId);
    }
}
