namespace LuminousStudio.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    using static LuminousStudio.Data.Common.EntityConstants.LampStyle;

    public class LampStyleConfiguration : IEntityTypeConfiguration<LampStyle>
    {
        public void Configure(EntityTypeBuilder<LampStyle> entity)
        {
            entity
                .HasKey(ls => ls.Id);

            // Define comment for the Id column
            entity
                .Property(ls => ls.Id)
                .HasComment("Primary key of the lamp style.");

            // Define constraints for the LampStyleName column
            entity
                .Property(ls => ls.LampStyleName)
                .IsRequired(true)
                .HasMaxLength(NameMaxLength)
                .HasComment("The name of the lamp style category.");

            entity
                .ToTable("LampStyles", t => t
                    .HasComment("Stores the available lamp style categories."));
        }
    }
}