namespace LuminousStudio.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using LuminousStudio.Services.Core.Contracts;
    using LuminousStudio.Services.Common;
    using LuminousStudio.Web.ViewModels.Statistic;

    [Authorize(Roles = ApplicationRoles.Administrator)]
    public class StatisticController : BaseController
    {
        private readonly IStatisticService _statisticsService;

        public StatisticController(IStatisticService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            StatisticVM statistics = new StatisticVM
            {
                CountClients = await _statisticsService.CountClientsAsync(),
                CountTiffanyLamps = await _statisticsService.CountTiffanyLampsAsync(),
                CountOrders = await _statisticsService.CountOrdersAsync(),
                SumOrders = await _statisticsService.SumOrdersAsync()
            };

            return View(statistics);
        }
    }
}
