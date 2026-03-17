namespace LuminousStudio.Core.Contracts
{
    public interface IStatisticService
    {
        int CountTiffanyLamps();
        int CountClients();
        int CountOrders();
        decimal SumOrders();
    }
}
