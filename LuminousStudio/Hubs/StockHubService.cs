namespace LuminousStudio.Web.Hubs
{
    using Microsoft.AspNetCore.SignalR;

    using LuminousStudio.Services.Common;

    public class StockHubService : IStockHubService
    {
        private readonly IHubContext<StockHub> _hubContext;

        public StockHubService(IHubContext<StockHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyStockUpdateAsync(Guid lampId, int newQuantity, string lampName)
        {
            await _hubContext.Clients.All.SendAsync(
                "ReceiveStockUpdate", lampId, newQuantity, lampName);
        }
    }
}