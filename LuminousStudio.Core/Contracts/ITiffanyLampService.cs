using LuminousStudio.Infrastructure.Data.Entities;

namespace LuminousStudio.Core.Contracts
{
    public interface ITiffanyLampService
    {
        bool Create(string name, int manufactorerId, int lampStyleId, string picture, int quantity, decimal price, decimal discount);

        bool Update(int tiffanyLampId, string name, int manufactorerId, int lampStyleId, string picture, int quantity, decimal price, decimal discount);

        List<TiffanyLamp> GetTiffanyLamps();

        TiffanyLamp GetTiffanyLampById(int tiffanyLampId);

        bool RemoveById(int tiffanyLampId);

        List<TiffanyLamp> GetTiffanyLamps(string searchStringLampStyleName, string searchStringManufactorerName);
    }
}
