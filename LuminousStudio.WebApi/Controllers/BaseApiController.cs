namespace LuminousStudio.WebApi.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        protected bool IsUserAuthenticated()
        {
            return User.Identity?.IsAuthenticated ?? false;
        }

        protected Guid? GetCurrentUserId()
        {
            if (!IsUserAuthenticated())
            {
                return null;
            }

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null)
            {
                return null;
            }

            return Guid.Parse(userIdString);
        }
    }
}