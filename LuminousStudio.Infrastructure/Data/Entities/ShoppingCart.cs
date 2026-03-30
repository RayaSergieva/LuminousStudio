namespace LuminousStudio.Infrastructure.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;

    [Comment("Stores shopping cart items for users before they place orders.")]
    public class ShoppingCart
    {
        [Comment("Primary key of the shopping cart item.")]
        public Guid Id { get; set; }

        [Comment("Foreign key to the selected Tiffany lamp.")]
        public Guid TiffanyLampId { get; set; }

        [ForeignKey(nameof(TiffanyLampId))]
        [Comment("Navigation property to the Tiffany lamp in the shopping cart.")]
        public virtual TiffanyLamp TiffanyLamp { get; set; } = null!;

        [Range(1, 1000)]
        [Comment("The quantity of the selected Tiffany lamp in the shopping cart.")]
        public int Count { get; set; }

        [Required]
        [Comment("Foreign key to the user who owns the shopping cart item.")]
        public Guid ApplicationUserId { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        [Comment("Navigation property to the user who owns the shopping cart item.")]
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

        [Range(typeof(decimal), "0.01", "1000000")]
        [Comment("The current effective unit price of the lamp in the shopping cart.")]
        public decimal Price { get; set; }
    }
}