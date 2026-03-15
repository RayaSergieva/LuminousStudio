using System.ComponentModel.DataAnnotations;

namespace LuminousStudio.Infrastructure.Data.Entities
{
    public class LampStyle
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string LampStyleName { get; set; } = null!;

        public virtual IEnumerable<TiffanyLamp> tiffanyLamps { get; set; } = new List<TiffanyLamp>();
    }
}
