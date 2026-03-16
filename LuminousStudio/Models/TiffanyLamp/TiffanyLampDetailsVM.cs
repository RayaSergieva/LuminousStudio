using System.ComponentModel.DataAnnotations;

namespace LuminousStudio.Models.TiffanyLamp
{
    public class TiffanyLampDetailsVM
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tiffany Lamp Name")]
        public string TiffanyLampName { get; set; } = null!;

        public int ManufacturerId { get; set; }
        [Display(Name = "Manufacturer")]
        public string ManufacturerName { get; set; } = null!;

        public int LampStyleId { get; set; }
        [Display(Name = "Lamp Style")]
        public string LampStyleName { get; set; } = null!;

        [Display(Name = "Picture")]
        public string Picture { get; set; } = null!;

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Discount")]
        public decimal Discount { get; set; }
    }
}
