namespace LuminousStudio.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    using static LuminousStudio.Data.Common.EntityConstants.ApplicationUser;

    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> entity)
        {
            entity
                .HasKey(u => u.Id);

            // Define constraints for the FirstName column
            entity
                .Property(u => u.FirstName)
                .IsRequired(true)
                .HasMaxLength(FirstNameMaxLength)
                .HasComment("The first name of the user.");

            // Define constraints for the LastName column
            entity
                .Property(u => u.LastName)
                .IsRequired(true)
                .HasMaxLength(LastNameMaxLength)
                .HasComment("The last name of the user.");

            // Define constraints for the Address column
            entity
                .Property(u => u.Address)
                .IsRequired(true)
                .HasMaxLength(AddressMaxLength)
                .HasComment("The delivery or contact address of the user.");

            entity
                .ToTable("AspNetUsers", t => t
                    .HasComment("Stores application users, including administrators and clients."));
        }
    }
}