namespace LuminousStudio.Core.Contracts
{
    public interface IStatisticService
    {
        Task<int> CountTiffanyLampsAsync();
        Task<int> CountClientsAsync();
        Task<int> CountOrdersAsync();
        Task<decimal> SumOrdersAsync();
    }
}
