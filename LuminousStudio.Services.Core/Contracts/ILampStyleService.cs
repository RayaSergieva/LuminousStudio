namespace LuminousStudio.Services.Core.Contracts
{
    using LuminousStudio.Data.Models;

    public interface ILampStyleService
    {
        Task<List<LampStyle>> GetLampStylesAsync();
        Task<LampStyle?> GetLampStyleByIdAsync(Guid lampStyleId);
        Task<List<TiffanyLamp>> GetProductsByLampStyleAsync(Guid lampStyleId);
    }
}
