namespace LuminousStudio.Web.Areas.Admin.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using LuminousStudio.Services.Common;

    [Area("Admin")]
    [Authorize(Roles = ApplicationRoles.Administrator)]
    public abstract class BaseAdminController : Controller
    {
        protected Guid? GetCurrentUserId()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null)
            {
                return null;
            }

            return Guid.Parse(userIdString);
        }
    }
}