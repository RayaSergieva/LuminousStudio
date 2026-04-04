namespace LuminousStudio.Models.Order
{
    using System.ComponentModel.DataAnnotations;

    public class OrderCreateVM
    {
        public Guid Id { get; set; }

        public DateTime OrderDate { get; set; }

        public Guid TiffanyLampId { get; set; }
        public string TiffanyLampName { get; set; } = null!;
        public int QuantityInStock { get; set; }
        public string? Picture { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
