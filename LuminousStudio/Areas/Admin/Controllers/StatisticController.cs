namespace LuminousStudio.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using LuminousStudio.Services.Core.Contracts;
    using LuminousStudio.Web.ViewModels.Statistic;

    public class StatisticController : BaseAdminController
    {
        private readonly IStatisticService _statisticService;

        public StatisticController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            StatisticVM statistics = new StatisticVM
            {
                CountClients = await _statisticService.CountClientsAsync(),
                CountTiffanyLamps = await _statisticService.CountTiffanyLampsAsync(),
                CountOrders = await _statisticService.CountOrdersAsync(),
                SumOrders = await _statisticService.SumOrdersAsync()
            };

            return View(statistics);
        }
    }
}