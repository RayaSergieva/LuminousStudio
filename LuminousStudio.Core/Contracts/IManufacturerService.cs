namespace LuminousStudio.Core.Contracts
{
    using LuminousStudio.Infrastructure.Data.Entities;

    public interface IManufacturerService
    {
        Task<List<Manufacturer>> GetManufacturersAsync();
        Task<Manufacturer?> GetManufacturerByIdAsync(Guid manufacturerId);
        Task<List<TiffanyLamp>> GetProductsByManufacturerAsync(Guid manufacturerId);
    }
}
