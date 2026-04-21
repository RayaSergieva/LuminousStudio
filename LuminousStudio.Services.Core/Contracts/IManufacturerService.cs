namespace LuminousStudio.Services.Core.Contracts
{
    using LuminousStudio.Data.Models;

    public interface IManufacturerService
    {
        Task<List<Manufacturer>> GetManufacturersAsync();
        Task<Manufacturer?> GetManufacturerByIdAsync(Guid manufacturerId);
        Task<List<TiffanyLamp>> GetProductsByManufacturerAsync(Guid manufacturerId);
    }
}
