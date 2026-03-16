using LuminousStudio.Models.LampStyle;
using LuminousStudio.Models.Manufactorer;
using System.ComponentModel.DataAnnotations;

namespace LuminousStudio.Models.TiffanyLamp
{
    public class TiffanyLampCreateVM
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "Tiffany Lamp Name")]
        public string TiffanyLampName { get; set; } = null!;

        [Required]
        [Display(Name = "Manufacturer")]
        public int ManufacturerId { get; set; }
        public virtual List<ManufacturerPairVM> Manufacturers { get; set; } = new List<ManufacturerPairVM>();

        [Required]
        [Display(Name = "LampStyle")]
        public int LampStyleId { get; set; }
        public virtual List<LampStylePairVM> LampStyles { get; set; } = new List<LampStylePairVM>();

        [Display(Name = "Picture")]
        public string Picture { get; set; } = null!;

        [Range(0, 5000)]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Discount")]
        public decimal Discount { get; set; }
    }
}
