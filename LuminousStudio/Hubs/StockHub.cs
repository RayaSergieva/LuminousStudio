namespace LuminousStudio.Web.Hubs
{
    using Microsoft.AspNetCore.SignalR;

    public class StockHub : Hub
    {
        public async Task NotifyStockUpdate(Guid lampId, int newQuantity, string lampName)
        {
            await Clients.All.SendAsync("ReceiveStockUpdate", lampId, newQuantity, lampName);
        }
    }
}