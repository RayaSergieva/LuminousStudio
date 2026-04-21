namespace LuminousStudio.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;

    using LuminousStudio.Web.ViewModels;

    public class HomeController : BaseController
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AboutUs()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contacts()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error(int? statusCode = null)
        {
            return statusCode switch
            {
                400 => View("~/Views/Shared/Errors/Error400.cshtml"),
                401 => View("~/Views/Shared/Errors/Error401.cshtml"),
                403 => View("~/Views/Shared/Errors/Error403.cshtml"),
                404 => View("~/Views/Shared/Errors/Error404.cshtml"),
                500 => View("~/Views/Shared/Errors/Error500.cshtml"),
                503 => View("~/Views/Shared/Errors/Error503.cshtml"),
                _ => View(new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                })
            };
        }
    }
}
