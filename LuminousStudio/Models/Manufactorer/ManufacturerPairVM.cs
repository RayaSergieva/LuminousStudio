using System.ComponentModel.DataAnnotations;

namespace LuminousStudio.Models.Manufactorer
{
    public class ManufacturerPairVM
    {
        public int Id { get; set; }

        [Display(Name = "Manufacturer")]
        public string Name { get; set; } = null!;
    }
}
