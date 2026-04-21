namespace LuminousStudio.Web.ViewModels.TiffanyLamp
{
    using System.ComponentModel.DataAnnotations;

    using LuminousStudio.Data.Common;
    using LuminousStudio.Web.ViewModels.LampStyle;
    using LuminousStudio.Web.ViewModels.Manufactorer;

    using static LuminousStudio.Web.Common.ValidationMessages.TiffanyLamp;

    public class TiffanyLampEditVM
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = NameRequiredMessage)]
        [MinLength(EntityConstants.TiffanyLamp.NameMinLength, ErrorMessage = NameMinLengthMessage)]
        [MaxLength(EntityConstants.TiffanyLamp.NameMaxLength, ErrorMessage = NameMaxLengthMessage)]
        [Display(Name = "Tiffany Lamp Name")]
        public string TiffanyLampName { get; set; } = null!;

        [Required(ErrorMessage = ManufacturerRequiredMessage)]
        [Display(Name = "Manufacturer")]
        public Guid ManufacturerId { get; set; }
        public virtual List<ManufacturerPairVM> Manufacturers { get; set; } = new List<ManufacturerPairVM>();

        [Required(ErrorMessage = LampStyleRequiredMessage)]
        [Display(Name = "Lamp Style")]
        public Guid LampStyleId { get; set; }
        public virtual List<LampStylePairVM> LampStyles { get; set; } = new List<LampStylePairVM>();

        [Required(ErrorMessage = PictureRequiredMessage)]
        [Display(Name = "Picture")]
        public string Picture { get; set; } = null!;

        [Range(EntityConstants.TiffanyLamp.QuantityMin, EntityConstants.TiffanyLamp.QuantityMax,
            ErrorMessage = QuantityRangeMessage)]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Range(typeof(decimal), EntityConstants.TiffanyLamp.PriceMin, EntityConstants.TiffanyLamp.PriceMax,
            ErrorMessage = PriceRangeMessage)]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Range(typeof(decimal), EntityConstants.TiffanyLamp.DiscountMin, EntityConstants.TiffanyLamp.DiscountMax,
            ErrorMessage = DiscountRangeMessage)]
        [Display(Name = "Discount")]
        public decimal Discount { get; set; }
    }
}