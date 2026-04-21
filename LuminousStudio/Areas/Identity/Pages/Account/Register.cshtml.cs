namespace LuminousStudio.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using LuminousStudio.Data.Common;
    using LuminousStudio.Data.Models;
    using LuminousStudio.Services.Common;

    using static LuminousStudio.Web.Common.ValidationMessages.ApplicationUser;

    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = null!;

        public string? ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = FirstNameRequiredMessage)]
            [StringLength(EntityConstants.ApplicationUser.FirstNameMaxLength,
                MinimumLength = EntityConstants.ApplicationUser.FirstNameMinLength,
                ErrorMessage = FirstNameMaxLengthMessage)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; } = null!;

            [Required(ErrorMessage = LastNameRequiredMessage)]
            [StringLength(EntityConstants.ApplicationUser.LastNameMaxLength,
                MinimumLength = EntityConstants.ApplicationUser.LastNameMinLength,
                ErrorMessage = LastNameMaxLengthMessage)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; } = null!;

            [Required(ErrorMessage = AddressRequiredMessage)]
            [StringLength(EntityConstants.ApplicationUser.AddressMaxLength,
                MinimumLength = EntityConstants.ApplicationUser.AddressMinLength,
                ErrorMessage = AddressMaxLengthMessage)]
            [Display(Name = "Address")]
            public string Address { get; set; } = null!;

            [Required(ErrorMessage = UsernameRequiredMessage)]
            [Display(Name = "Username")]
            public string UserName { get; set; } = null!;

            [Required(ErrorMessage = EmailRequiredMessage)]
            [EmailAddress(ErrorMessage = EmailInvalidMessage)]
            [Display(Name = "Email")]
            public string Email { get; set; } = null!;

            [Required(ErrorMessage = PasswordRequiredMessage)]
            [StringLength(EntityConstants.ApplicationUser.PasswordMaxLength,
                MinimumLength = EntityConstants.ApplicationUser.PasswordMinLength,
                ErrorMessage = PasswordMinLengthMessage)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; } = null!;

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = PasswordMismatchMessage)]
            public string ConfirmPassword { get; set; } = null!;
        }

        public async Task OnGetAsync(string? returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.UserName,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Address = Input.Address
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, ApplicationRoles.Client);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}