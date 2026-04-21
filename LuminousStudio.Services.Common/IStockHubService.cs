namespace LuminousStudio.Services.Common
{
    public interface IStockHubService
    {
        Task NotifyStockUpdateAsync(Guid lampId, int newQuantity, string lampName);
    }
}