namespace LuminousStudio.Services.Admin.Contracts
{
    using LuminousStudio.Data.Models;

    public interface IUserManagementService
    {
        Task<IEnumerable<ApplicationUser>> GetAllClientsAsync();
        Task<ApplicationUser?> GetClientByIdAsync(Guid id);
        Task<bool> DeleteClientAsync(Guid id);
        Task<bool> ClientHasOrdersAsync(Guid id);
    }
}