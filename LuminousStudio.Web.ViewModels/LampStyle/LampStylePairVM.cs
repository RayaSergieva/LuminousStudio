namespace LuminousStudio.Web.ViewModels.LampStyle
{
    using System.ComponentModel.DataAnnotations;

    public class LampStylePairVM
    {
        public Guid Id { get; set; }

        [Display(Name = "LampStyle")]
        public string Name { get; set; } = null!;
    }
}
