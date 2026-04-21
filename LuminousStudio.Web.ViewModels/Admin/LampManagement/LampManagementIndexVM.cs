namespace LuminousStudio.Web.ViewModels.Admin.LampManagement
{
    public class LampManagementIndexVM
    {
        public Guid Id { get; set; }
        public string TiffanyLampName { get; set; } = null!;
        public string ManufacturerName { get; set; } = null!;
        public string LampStyleName { get; set; } = null!;
        public string Picture { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}