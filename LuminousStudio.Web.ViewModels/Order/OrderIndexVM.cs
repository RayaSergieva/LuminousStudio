namespace LuminousStudio.Web.ViewModels.Order
{
    public class OrderIndexVM
    {
        public Guid Id { get; set; }
        public string OrderDate { get; set; } = null!;
        public Guid UserId { get; set; }
        public string User { get; set; } = null!;

        public Guid TiffanyLampId { get; set; }
        public string TiffanyLamp { get; set; } = null!;
        public string Picture { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
