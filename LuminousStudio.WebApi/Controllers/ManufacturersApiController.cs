namespace LuminousStudio.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using LuminousStudio.Services.Core.Contracts;

    public class ManufacturersApiController : BaseApiController
    {
        private readonly IManufacturerService _manufacturerService;

        public ManufacturersApiController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var manufacturers = await _manufacturerService.GetManufacturersAsync();

            var result = manufacturers.Select(m => new
            {
                m.Id,
                m.ManufacturerName
            });

            return Ok(result);
        }
    }
}