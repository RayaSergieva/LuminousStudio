namespace LuminousStudio.WebApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using LuminousStudio.Services.Core.Contracts;
    using LuminousStudio.Services.Common;

    public class StatisticsApiController : BaseApiController
    {
        private readonly IStatisticService _statisticService;

        public StatisticsApiController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        [HttpGet]
        [Authorize(Roles = ApplicationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetStatistics()
        {
            var result = new
            {
                CountClients = await _statisticService.CountClientsAsync(),
                CountTiffanyLamps = await _statisticService.CountTiffanyLampsAsync(),
                CountOrders = await _statisticService.CountOrdersAsync(),
                TotalRevenue = await _statisticService.SumOrdersAsync()
            };

            return Ok(result);
        }
    }
}