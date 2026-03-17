using LuminousStudio.Core.Contracts;
using LuminousStudio.Models.Statistic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuminousStudio.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class StatisticController : Controller
    {
        private readonly IStatisticService statisticsService;

        public StatisticController(IStatisticService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        public IActionResult Index()
        {
            StatisticVM statistics = new StatisticVM();

            statistics.CountClients = statisticsService.CountClients();
            statistics.CountTiffanyLamps = statisticsService.CountTiffanyLamps();
            statistics.CountOrders = statisticsService.CountOrders();
            statistics.SumOrders = statisticsService.SumOrders();

            return View(statistics);
        }
    }
}
