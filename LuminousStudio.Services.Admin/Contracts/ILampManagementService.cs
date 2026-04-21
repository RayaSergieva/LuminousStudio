namespace LuminousStudio.Services.Admin.Contracts
{
    using LuminousStudio.Data.Models;

    public interface ILampManagementService
    {
        Task<IEnumerable<TiffanyLamp>> GetAllLampsAsync();
        Task<TiffanyLamp?> GetLampByIdAsync(Guid id);
        Task<bool> CreateLampAsync(string name, Guid manufacturerId, Guid lampStyleId,
            string picture, int quantity, decimal price, decimal discount);
        Task<bool> UpdateLampAsync(Guid id, string name, Guid manufacturerId, Guid lampStyleId,
            string picture, int quantity, decimal price, decimal discount);
        Task<bool> DeleteLampAsync(Guid id);
    }
}