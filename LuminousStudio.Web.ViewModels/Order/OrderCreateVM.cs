namespace LuminousStudio.Web.ViewModels.Order
{
    using System.ComponentModel.DataAnnotations;

    using LuminousStudio.Data.Common;

    using static LuminousStudio.Web.Common.ValidationMessages.Order;

    public class OrderCreateVM
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid TiffanyLampId { get; set; }
        public string TiffanyLampName { get; set; } = null!;
        public int QuantityInStock { get; set; }
        public string? Picture { get; set; }

        [Range(EntityConstants.Order.QuantityMin, EntityConstants.Order.QuantityMax,
            ErrorMessage = QuantityRangeMessage)]
        public int Quantity { get; set; }

        [Range(typeof(decimal), EntityConstants.Order.PriceMin, EntityConstants.Order.PriceMax,
            ErrorMessage = PriceRangeMessage)]
        public decimal Price { get; set; }

        [Range(typeof(decimal), EntityConstants.Order.DiscountMin, EntityConstants.Order.DiscountMax,
            ErrorMessage = DiscountRangeMessage)]
        public decimal Discount { get; set; }

        public decimal TotalPrice { get; set; }
    }
}