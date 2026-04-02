namespace LuminousStudio.Core.Contracts
{
    using LuminousStudio.Infrastructure.Data.Entities;

    public interface ITiffanyLampService
    {
        Task<bool> CreateAsync(string name, Guid manufactorerId, Guid lampStyleId, string picture, int quantity, decimal price, decimal discount);

        Task<bool> UpdateAsync(Guid tiffanyLampId, string name, Guid manufactorerId, Guid lampStyleId, string picture, int quantity, decimal price, decimal discount);

        Task<List<TiffanyLamp>> GetTiffanyLampsAsync();

        Task<TiffanyLamp?> GetTiffanyLampByIdAsync(Guid tiffanyLampId);

        Task<bool> RemoveByIdAsync(Guid tiffanyLampId);

        Task<List<TiffanyLamp>> GetTiffanyLampsAsync(string searchStringLampStyleName, string searchStringManufactorerName);
    }
}
