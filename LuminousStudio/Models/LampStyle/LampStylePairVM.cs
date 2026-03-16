using System.ComponentModel.DataAnnotations;

namespace LuminousStudio.Models.LampStyle
{
    public class LampStylePairVM
    {
        public int Id { get; set; }

        [Display(Name = "LampStyle")]
        public string Name { get; set; } = null!;
    }
}
