namespace LuminousStudio.Web.Infrastructure.Extensions
{
    using System.Reflection;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using LuminousStudio.Data;
    using LuminousStudio.Data.Models;
    using LuminousStudio.Data.Seeding;
    using LuminousStudio.Data.Seeding.Interfaces;
    using LuminousStudio.Web.Common.Configuration;
    using LuminousStudio.Services.Admin.Contracts;
    using LuminousStudio.Services.Admin.Services;

    public static class ServiceCollectionExtension
    {
        private const string ServiceTypeSuffix = "Service";
        private const string RepositoryTypeSuffix = "Repository";
        private const string ProjectInterfacePrefix = "I";

        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration
                .GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }

        public static IServiceCollection AddApplicationIdentity(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<AdminSettings>(
                configuration.GetSection("AdminSettings"));

            services.Configure<IdentitySettings>(
                configuration.GetSection("IdentitySettings"));

            services
                .AddDefaultIdentity<ApplicationUser>(options =>
                {
                    ConfigureIdentity(configuration, options);
                })
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }

        public static IServiceCollection AddUserDefinedServices(
            this IServiceCollection services, Assembly serviceAssembly)
        {
            Type[] serviceClasses = serviceAssembly
                .GetTypes()
                .Where(t => !t.IsInterface &&
                            t.Name.EndsWith(ServiceTypeSuffix))
                .ToArray();

            foreach (Type serviceClass in serviceClasses)
            {
                Type? serviceInterface = serviceClass
                    .GetInterfaces()
                    .FirstOrDefault(i =>
                        i.Name == $"{ProjectInterfacePrefix}{serviceClass.Name}");

                if (serviceInterface == null)
                {
                    throw new ArgumentException(
                        $"Interface not found for {serviceClass.Name}");
                }

                services.AddScoped(serviceInterface, serviceClass);
            }

            return services;
        }

        public static IServiceCollection AddRepositories(
            this IServiceCollection services, Assembly repositoryAssembly)
        {
            Type[] repositoryClasses = repositoryAssembly
                .GetTypes()
                .Where(t => t.Name.EndsWith(RepositoryTypeSuffix) &&
                            !t.IsInterface &&
                            !t.IsAbstract)
                .ToArray();

            foreach (Type repositoryClass in repositoryClasses)
            {
                Type? repositoryInterface = repositoryClass
                    .GetInterfaces()
                    .FirstOrDefault(i =>
                        i.Name == $"{ProjectInterfacePrefix}{repositoryClass.Name}");

                if (repositoryInterface == null)
                {
                    throw new ArgumentException(
                        $"Interface not found for {repositoryClass.Name}");
                }

                services.AddScoped(repositoryInterface, repositoryClass);
            }

            return services;
        }

        public static IServiceCollection AddSeeders(
            this IServiceCollection services)
        {
            services.AddScoped<IIdentitySeeder, IdentitySeeder>();

            return services;
        }

        public static IServiceCollection AddAdminServices(
            this IServiceCollection services)
        {
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddScoped<ILampManagementService, LampManagementService>();

            return services;
        }

        private static void ConfigureIdentity(
            IConfiguration configuration, IdentityOptions options)
        {
            options.SignIn.RequireConfirmedAccount =
                configuration.GetValue<bool>("IdentitySettings:RequireConfirmedAccount");
            options.Password.RequireDigit =
                configuration.GetValue<bool>("IdentitySettings:RequireDigit");
            options.Password.RequireLowercase =
                configuration.GetValue<bool>("IdentitySettings:RequireLowercase");
            options.Password.RequireUppercase =
                configuration.GetValue<bool>("IdentitySettings:RequireUppercase");
            options.Password.RequireNonAlphanumeric =
                configuration.GetValue<bool>("IdentitySettings:RequireNonAlphanumeric");
            options.Password.RequiredLength =
                configuration.GetValue<int>("IdentitySettings:RequiredLength");
        }
    }
}