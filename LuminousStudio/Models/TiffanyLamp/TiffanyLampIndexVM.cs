using System.ComponentModel.DataAnnotations;

namespace LuminousStudio.Models.TiffanyLamp
{
    public class TiffanyLampIndexVM
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tiffany Lamp Name")]
        public string TiffanyLampName { get; set; }

        [Display(Name = "Manufacturer")]
        public string ManufacturerName { get; set; }

        [Display(Name = "Lamp Style")]
        public string LampStyleName { get; set; }

        [Display(Name = "Picture")]
        public string Picture { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Discount")]
        public decimal Discount { get; set; }
    }
}
