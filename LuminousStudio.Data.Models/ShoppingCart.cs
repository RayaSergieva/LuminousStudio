namespace LuminousStudio.Data.Models
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }

        public Guid TiffanyLampId { get; set; }

        public virtual TiffanyLamp TiffanyLamp { get; set; } = null!;

        public int Count { get; set; }

        public Guid ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

        public decimal Price { get; set; }
    }
}