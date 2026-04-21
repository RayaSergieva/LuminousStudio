namespace LuminousStudio.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using LuminousStudio.Services.Core.Contracts;

    public class LampStylesApiController : BaseApiController
    {
        private readonly ILampStyleService _lampStyleService;

        public LampStylesApiController(ILampStyleService lampStyleService)
        {
            _lampStyleService = lampStyleService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var styles = await _lampStyleService.GetLampStylesAsync();

            var result = styles.Select(s => new
            {
                s.Id,
                s.LampStyleName
            });

            return Ok(result);
        }
    }
}