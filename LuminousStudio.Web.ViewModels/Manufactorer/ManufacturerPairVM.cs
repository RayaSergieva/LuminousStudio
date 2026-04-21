namespace LuminousStudio.Web.ViewModels.Manufactorer
{
    using System.ComponentModel.DataAnnotations;

    public class ManufacturerPairVM
    {
        public Guid Id { get; set; }

        [Display(Name = "Manufacturer")]
        public string Name { get; set; } = null!;
    }
}
