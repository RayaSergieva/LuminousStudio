using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LuminousStudio.Infrastructure.Data.Entities
{
    [Comment("Stores the manufacturers or designers associated with Tiffany lamps.")]
    public class Manufacturer
    {
        [Comment("Primary key of the manufacturer.")]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Comment("The full name of the manufacturer or designer.")]
        public string ManufacturerName { get; set; } = null!;

        [Comment("Collection of Tiffany lamps associated with this manufacturer.")]
        public virtual IEnumerable<TiffanyLamp> tiffanyLamps { get; set; } = new List<TiffanyLamp>();
    }
}