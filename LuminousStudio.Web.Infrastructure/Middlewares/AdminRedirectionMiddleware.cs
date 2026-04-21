namespace LuminousStudio.Web.Infrastructure.Middlewares
{
    using Microsoft.AspNetCore.Http;

    using LuminousStudio.Services.Common;

    public class AdminRedirectionMiddleware
    {
        private const string IndexPath = "/";
        private const string AdminIndexPath = "/Admin";

        private readonly RequestDelegate _next;

        public AdminRedirectionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity?.IsAuthenticated == true &&
                context.Request.Path == IndexPath &&
                context.User.IsInRole(ApplicationRoles.Administrator))
            {
                context.Response.Redirect(AdminIndexPath);
                return;
            }

            await _next(context);
        }
    }
}