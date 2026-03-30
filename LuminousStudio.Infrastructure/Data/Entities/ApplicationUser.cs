namespace LuminousStudio.Infrastructure.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    [Comment("Stores application users, including administrators and clients.")]
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Required]
        [MaxLength(30)]
        [Comment("The first name of the user.")]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(30)]
        [Comment("The last name of the user.")]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        [Comment("The delivery or contact address of the user.")]
        public string Address { get; set; } = null!;
    }
}