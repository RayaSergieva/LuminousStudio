using LuminousStudio.Infrastructure.Data.Entities;

namespace LuminousStudio.Core.Contracts
{
    public interface IManufacturerService
    {
        List<Manufacturer> GetManufacturers();
        Manufacturer GetManufacturerById(int manufacturerId);
        List<TiffanyLamp> GetProductsByManufacturer(int manufacturerId);
    }
}
