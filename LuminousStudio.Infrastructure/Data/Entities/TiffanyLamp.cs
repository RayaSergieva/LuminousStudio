using System.ComponentModel.DataAnnotations;

namespace LuminousStudio.Infrastructure.Data.Entities
{
    public class TiffanyLamp
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]

        public string TiffanyLampName { get; set; } = null!;

        [Required]
        public int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; } = null!;
        [Required]

        public int LampStyleId { get; set; }
        public virtual LampStyle LampStyle { get; set; } = null!;
        public string Picture { get; set; } = null!;

        [Range(0, 5000)]
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public virtual IEnumerable<Order> Orders { get; set; } = new List<Order>();
    }
}
