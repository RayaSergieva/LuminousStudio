namespace LuminousStudio.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> entity)
        {
            entity
                .HasKey(sc => sc.Id);

            // Define comment for the Id column
            entity
                .Property(sc => sc.Id)
                .HasComment("Primary key of the shopping cart item.");

            // Define comment for the TiffanyLampId column
            entity
                .Property(sc => sc.TiffanyLampId)
                .HasComment("Foreign key to the selected Tiffany lamp.");

            // Define comment for the ApplicationUserId column
            entity
                .Property(sc => sc.ApplicationUserId)
                .HasComment("Foreign key to the user who owns the shopping cart item.");

            // Define constraints for the Count column
            entity
                .Property(sc => sc.Count)
                .IsRequired(true)
                .HasComment("The quantity of the selected Tiffany lamp in the shopping cart.");

            // Define constraints for the Price column
            entity
                .Property(sc => sc.Price)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)")
                .HasComment("The current effective unit price of the lamp in the shopping cart.");

            // Configure relation between ShoppingCart and TiffanyLamp
            entity
                .HasOne(sc => sc.TiffanyLamp)
                .WithMany()
                .HasForeignKey(sc => sc.TiffanyLampId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relation between ShoppingCart and ApplicationUser
            entity
                .HasOne(sc => sc.ApplicationUser)
                .WithMany()
                .HasForeignKey(sc => sc.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .ToTable("ShoppingCarts", t => t
                    .HasComment("Stores shopping cart items for users before they place orders."));
        }
    }
}