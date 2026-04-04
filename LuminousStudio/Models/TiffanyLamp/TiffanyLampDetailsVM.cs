namespace LuminousStudio.Models.TiffanyLamp
{
    using System.ComponentModel.DataAnnotations;

    public class TiffanyLampDetailsVM
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Tiffany Lamp Name")]
        public string TiffanyLampName { get; set; } = null!;

        public Guid ManufacturerId { get; set; }
        [Display(Name = "Manufacturer")]
        public string ManufacturerName { get; set; } = null!;

        public Guid LampStyleId { get; set; }
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
