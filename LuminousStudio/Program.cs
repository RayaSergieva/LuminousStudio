using LuminousStudio.Data.Repository;
using LuminousStudio.Services.Common;
using LuminousStudio.Services.Core.Services;
using LuminousStudio.Web.Hubs;
using LuminousStudio.Web.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddApplicationIdentity(builder.Configuration);
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddScoped<IStockHubService, StockHubService>();
builder.Services.AddRepositories(typeof(TiffanyLampRepository).Assembly);
builder.Services.AddUserDefinedServices(typeof(TiffanyLampService).Assembly);
builder.Services.AddSeeders();
builder.Services.AddAdminServices();

var app = builder.Build();

app.SeedDefaultIdentity();
await app.PrepareDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseGlobalExceptionHandling();
    app.UseExceptionHandler("/Home/Error?statusCode=500");
    app.UseHsts();
}

app.UseSecurityHeaders();
app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAdminRedirection();
app.MapHub<StockHub>("/stockHub");
app.MapStaticAssets();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();