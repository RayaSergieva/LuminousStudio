namespace LuminousStudio.Data.Models
{
    public class TiffanyLamp
    {
        public Guid Id { get; set; }

        public string TiffanyLampName { get; set; } = null!;

        public Guid ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; } = null!;

        public Guid LampStyleId { get; set; }

        public virtual LampStyle LampStyle { get; set; } = null!;

        public string Picture { get; set; } = null!;

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}