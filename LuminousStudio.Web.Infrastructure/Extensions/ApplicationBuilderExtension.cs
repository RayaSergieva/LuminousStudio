namespace LuminousStudio.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    using LuminousStudio.Data;
    using LuminousStudio.Data.Seeding;
    using LuminousStudio.Data.Seeding.Interfaces;
    using LuminousStudio.Web.Infrastructure.Middlewares;

    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder SeedDefaultIdentity(
            this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            IServiceProvider serviceProvider = scope.ServiceProvider;

            IIdentitySeeder identitySeeder = serviceProvider
                .GetRequiredService<IIdentitySeeder>();

            identitySeeder
                .SeedIdentityAsync()
                .GetAwaiter()
                .GetResult();

            return app;
        }

        public static async Task<IApplicationBuilder> PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            var dbContext = services.GetRequiredService<ApplicationDbContext>();

            var lampStyleSeeder = new LampStyleSeeder(dbContext);
            await lampStyleSeeder.SeedAsync();

            var manufacturerSeeder = new ManufacturerSeeder(dbContext);
            await manufacturerSeeder.SeedAsync();

            return app;
        }

        public static IApplicationBuilder UseGlobalExceptionHandling(
            this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionMiddleware>();
            return app;
        }

        public static IApplicationBuilder UseSecurityHeaders(
            this IApplicationBuilder app)
        {
            app.UseMiddleware<SecurityHeadersMiddleware>();
            return app;
        }

        public static IApplicationBuilder UseAdminRedirection(
            this IApplicationBuilder app)
        {
            app.UseMiddleware<AdminRedirectionMiddleware>();
            return app;
        }
    }
}