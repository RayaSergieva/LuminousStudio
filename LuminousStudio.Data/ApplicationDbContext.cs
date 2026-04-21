namespace LuminousStudio.Data
{
    using System.Reflection;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using LuminousStudio.Data.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Manufacturer> Manufacturers { get; set; }

        public virtual DbSet<LampStyle> LampStyles { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<TiffanyLamp> TiffanyLamps { get; set; }

        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}