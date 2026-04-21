namespace LuminousStudio.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using LuminousStudio.Data.Common;
    using Models;

    public class TiffanyLampConfiguration : IEntityTypeConfiguration<TiffanyLamp>
    {
        public void Configure(EntityTypeBuilder<TiffanyLamp> entity)
        {
            entity
                .HasKey(tl => tl.Id);

            // Define comment for the Id column
            entity
                .Property(tl => tl.Id)
                .HasComment("Primary key of the Tiffany lamp.");

            // Define constraints for the TiffanyLampName column
            entity
                .Property(tl => tl.TiffanyLampName)
                .IsRequired(true)
                .HasMaxLength(EntityConstants.TiffanyLamp.NameMaxLength)
                .HasComment("The display name of the Tiffany lamp.");

            // Define constraints for the Picture column
            entity
                .Property(tl => tl.Picture)
                .IsRequired(true)
                .HasComment("URL or path to the image of the Tiffany lamp.");

            // Define constraints for the Quantity column
            entity
                .Property(tl => tl.Quantity)
                .IsRequired(true)
                .HasComment("The available quantity in stock.");

            // Define constraints for the Price column
            entity
                .Property(tl => tl.Price)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)")
                .HasComment("The standard price of the Tiffany lamp.");

            // Define constraints for the Discount column
            entity
                .Property(tl => tl.Discount)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)")
                .HasComment("The discount percentage applied to the Tiffany lamp.");

            // Define comment for the ManufacturerId column
            entity
                .Property(tl => tl.ManufacturerId)
                .HasComment("Foreign key to the manufacturer of the Tiffany lamp.");

            // Define comment for the LampStyleId column
            entity
                .Property(tl => tl.LampStyleId)
                .HasComment("Foreign key to the lamp style of the Tiffany lamp.");

            // Configure relation between TiffanyLamp and Manufacturer
            entity
                .HasOne(tl => tl.Manufacturer)
                .WithMany(m => m.TiffanyLamps)
                .HasForeignKey(tl => tl.ManufacturerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relation between TiffanyLamp and LampStyle
            entity
                .HasOne(tl => tl.LampStyle)
                .WithMany(ls => ls.TiffanyLamps)
                .HasForeignKey(tl => tl.LampStyleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .ToTable("TiffanyLamps", t => t
                    .HasComment("Stores the main Tiffany lamp products offered in the application."));
        }
    }
}