namespace LuminousStudio.Models.Client
{
    using System.ComponentModel.DataAnnotations;

    public class ClientIndexVM
    {
        public Guid Id { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; } = null!;

        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool IsAdmin { get; set; }
    }
}
