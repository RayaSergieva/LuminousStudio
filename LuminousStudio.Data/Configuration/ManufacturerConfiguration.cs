namespace LuminousStudio.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    using static LuminousStudio.Data.Common.EntityConstants.Manufacturer;

    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> entity)
        {
            entity
                .HasKey(m => m.Id);

            // Define comment for the Id column
            entity
                .Property(m => m.Id)
                .HasComment("Primary key of the manufacturer.");

            // Define constraints for the ManufacturerName column
            entity
                .Property(m => m.ManufacturerName)
                .IsRequired(true)
                .HasMaxLength(NameMaxLength)
                .HasComment("The full name of the manufacturer or designer.");

            entity
                .ToTable("Manufacturers", t => t
                    .HasComment("Stores the manufacturers or designers associated with Tiffany lamps."));
        }
    }
}