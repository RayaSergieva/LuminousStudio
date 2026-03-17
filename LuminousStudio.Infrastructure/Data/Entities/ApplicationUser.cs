using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LuminousStudio.Infrastructure.Data.Entities
{
    [Comment("Stores application users, including administrators and clients.")]
    public class ApplicationUser : IdentityUser
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