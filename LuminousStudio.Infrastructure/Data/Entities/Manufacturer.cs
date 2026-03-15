using System.ComponentModel.DataAnnotations;

namespace LuminousStudio.Infrastructure.Data.Entities
{
    public class Manufacturer
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string ManufacturerName { get; set; } = null!;

        public virtual IEnumerable<TiffanyLamp> tiffanyLamps { get; set; } = new List<TiffanyLamp>();
    }
}
