using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LuminousStudio.Infrastructure.Data.Entities
{
    [Comment("Stores the main Tiffany lamp products offered in the application.")]
    public class TiffanyLamp
    {
        [Comment("Primary key of the Tiffany lamp.")]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Comment("The display name of the Tiffany lamp.")]
        public string TiffanyLampName { get; set; } = null!;

        [Required]
        [Comment("Foreign key to the manufacturer of the Tiffany lamp.")]
        public int ManufacturerId { get; set; }

        [Comment("Navigation property to the manufacturer of the Tiffany lamp.")]
        public virtual Manufacturer Manufacturer { get; set; } = null!;

        [Required]
        [Comment("Foreign key to the lamp style of the Tiffany lamp.")]
        public int LampStyleId { get; set; }

        [Comment("Navigation property to the lamp style of the Tiffany lamp.")]
        public virtual LampStyle LampStyle { get; set; } = null!;

        [Required]
        [Comment("URL or path to the image of the Tiffany lamp.")]
        public string Picture { get; set; } = null!;

        [Range(0, 5000)]
        [Comment("The available quantity in stock.")]
        public int Quantity { get; set; }

        [Range(typeof(decimal), "0.01", "1000000")]
        [Comment("The standard price of the Tiffany lamp.")]
        public decimal Price { get; set; }

        [Range(typeof(decimal), "0", "100")]
        [Comment("The discount percentage applied to the Tiffany lamp.")]
        public decimal Discount { get; set; }

        [Comment("Collection of orders associated with this Tiffany lamp.")]
        public virtual IEnumerable<Order> Orders { get; set; } = new List<Order>();
    }
}