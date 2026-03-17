using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LuminousStudio.Infrastructure.Data.Entities
{
    [Comment("Stores customer orders for Tiffany lamps.")]
    public class Order
    {
        [Comment("Primary key of the order.")]
        public int Id { get; set; }

        [Required]
        [Comment("The date and time when the order was created.")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Comment("Foreign key to the ordered Tiffany lamp.")]
        public int TiffanyLampId { get; set; }

        [Comment("Navigation property to the ordered Tiffany lamp.")]
        public virtual TiffanyLamp TiffanyLamp { get; set; } = null!;

        [Required]
        [Comment("Foreign key to the user who placed the order.")]
        public string UserId { get; set; } = null!;

        [Comment("Navigation property to the user who placed the order.")]
        public virtual ApplicationUser User { get; set; } = null!;

        [Range(1, 1000)]
        [Comment("The quantity of lamps included in the order.")]
        public int Quantity { get; set; }

        [Range(typeof(decimal), "0.01", "1000000")]
        [Comment("The unit price of the lamp at the time of the order.")]
        public decimal Price { get; set; }

        [Range(typeof(decimal), "0", "100")]
        [Comment("The discount percentage applied to the order.")]
        public decimal Discount { get; set; }

        [Comment("The calculated total price for the order after discount.")]
        public decimal TotalPrice
        {
            get
            {
                return this.Quantity * this.Price - this.Quantity * this.Price * this.Discount / 100;
            }
        }
    }
}