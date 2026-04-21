namespace LuminousStudio.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using LuminousStudio.Services.Core.Contracts;

    public class TiffanyLampsApiController : BaseApiController
    {
        private readonly ITiffanyLampService _tiffanyLampService;

        public TiffanyLampsApiController(ITiffanyLampService tiffanyLampService)
        {
            _tiffanyLampService = tiffanyLampService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var lamps = await _tiffanyLampService.GetTiffanyLampsAsync();

            var result = lamps.Select(l => new
            {
                l.Id,
                l.TiffanyLampName,
                ManufacturerName = l.Manufacturer.ManufacturerName,
                LampStyleName = l.LampStyle.LampStyleName,
                l.Picture,
                l.Quantity,
                l.Price,
                l.Discount
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var lamp = await _tiffanyLampService.GetTiffanyLampByIdAsync(id);
            if (lamp == null)
            {
                return NotFound();
            }

            var result = new
            {
                lamp.Id,
                lamp.TiffanyLampName,
                ManufacturerName = lamp.Manufacturer.ManufacturerName,
                LampStyleName = lamp.LampStyle.LampStyleName,
                lamp.Picture,
                lamp.Quantity,
                lamp.Price,
                lamp.Discount
            };

            return Ok(result);
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Search(
            string? lampStyleName = null,
            string? manufacturerName = null)
        {
            var lamps = await _tiffanyLampService
                .GetTiffanyLampsAsync(
                    lampStyleName ?? string.Empty,
                    manufacturerName ?? string.Empty);

            var result = lamps.Select(l => new
            {
                l.Id,
                l.TiffanyLampName,
                ManufacturerName = l.Manufacturer.ManufacturerName,
                LampStyleName = l.LampStyle.LampStyleName,
                l.Picture,
                l.Quantity,
                l.Price,
                l.Discount
            });

            return Ok(result);
        }

        [HttpGet("style/{styleName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByStyle(string styleName)
        {
            var lamps = await _tiffanyLampService
                .GetTiffanyLampsAsync(styleName, string.Empty);

            var result = lamps.Select(l => new
            {
                l.Id,
                l.TiffanyLampName,
                ManufacturerName = l.Manufacturer.ManufacturerName,
                LampStyleName = l.LampStyle.LampStyleName,
                l.Picture,
                l.Quantity,
                l.Price,
                l.Discount
            });

            return Ok(result);
        }

        [HttpGet("discounted")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDiscounted()
        {
            var lamps = await _tiffanyLampService.GetTiffanyLampsAsync();

            var result = lamps
                .Where(l => l.Discount > 0)
                .Select(l => new
                {
                    l.Id,
                    l.TiffanyLampName,
                    ManufacturerName = l.Manufacturer.ManufacturerName,
                    LampStyleName = l.LampStyle.LampStyleName,
                    l.Picture,
                    l.Quantity,
                    l.Price,
                    l.Discount,
                    DiscountedPrice = l.Price - (l.Price * l.Discount / 100)
                });

            return Ok(result);
        }
    }
}