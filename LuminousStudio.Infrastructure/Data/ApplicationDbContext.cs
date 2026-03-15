using LuminousStudio.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuminousStudio.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Manufacturer> Manufacturers { get; set; } = null!;
        public DbSet<LampStyle> LampStyles { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<TiffanyLamp> TiffanyLamps { get; set; } = null!;
        public DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;
    }
}