namespace LuminousStudio.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Address { get; set; } = null!;
    }
}