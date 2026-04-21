namespace LuminousStudio.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entity)
        {
            entity
                .HasKey(o => o.Id);

            // Define comment for the Id column
            entity
                .Property(o => o.Id)
                .HasComment("Primary key of the order.");

            // Define comment for the TiffanyLampId column
            entity
                .Property(o => o.TiffanyLampId)
                .HasComment("Foreign key to the ordered Tiffany lamp.");

            // Define comment for the UserId column
            entity
                .Property(o => o.UserId)
                .HasComment("Foreign key to the user who placed the order.");

            // Define constraints for the OrderDate column
            entity
                .Property(o => o.OrderDate)
                .IsRequired(true)
                .HasComment("The date and time when the order was created.");

            // Define constraints for the Quantity column
            entity
                .Property(o => o.Quantity)
                .IsRequired(true)
                .HasComment("The quantity of lamps included in the order.");

            // Define constraints for the Price column
            entity
                .Property(o => o.Price)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)")
                .HasComment("The unit price of the lamp at the time of the order.");

            // Define constraints for the Discount column
            entity
                .Property(o => o.Discount)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)")
                .HasComment("The discount percentage applied to the order.");

            // Configure relation between Order and TiffanyLamp
            entity
                .HasOne(o => o.TiffanyLamp)
                .WithMany(tl => tl.Orders)
                .HasForeignKey(o => o.TiffanyLampId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relation between Order and ApplicationUser
            entity
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .ToTable("Orders", t => t
                    .HasComment("Stores customer orders for Tiffany lamps."));
        }
    }
}