namespace LuminousStudio.Infrastructure.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore;

    [Comment("Stores the manufacturers or designers associated with Tiffany lamps.")]
    public class Manufacturer
    {
        [Comment("Primary key of the manufacturer.")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Comment("The full name of the manufacturer or designer.")]
        public string ManufacturerName { get; set; } = null!;

        [Comment("Collection of Tiffany lamps associated with this manufacturer.")]
        public virtual ICollection<TiffanyLamp> TiffanyLamps { get; set; } = new List<TiffanyLamp>();
    }
}