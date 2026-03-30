namespace LuminousStudio.Infrastructure.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore;

    [Comment("Stores customer orders for Tiffany lamps.")]
    public class Order
    {
        [Comment("Primary key of the order.")]
        public Guid Id { get; set; }

        [Required]
        [Comment("The date and time when the order was created.")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Comment("Foreign key to the ordered Tiffany lamp.")]
        public Guid TiffanyLampId { get; set; }

        [Comment("Navigation property to the ordered Tiffany lamp.")]
        public virtual TiffanyLamp TiffanyLamp { get; set; } = null!;

        [Required]
        [Comment("Foreign key to the user who placed the order.")]
        public Guid UserId { get; set; }

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
                return Quantity * Price - Quantity * Price * Discount / 100;
            }
        }
    }
}