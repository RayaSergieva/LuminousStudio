namespace LuminousStudio.Data.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public DateTime OrderDate { get; set; }

        public Guid TiffanyLampId { get; set; }

        public virtual TiffanyLamp TiffanyLamp { get; set; } = null!;

        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalPrice
        {
            get
            {
                return Quantity * Price - Quantity * Price * Discount / 100;
            }
        }
    }
}