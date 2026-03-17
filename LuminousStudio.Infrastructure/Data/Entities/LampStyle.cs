using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LuminousStudio.Infrastructure.Data.Entities
{
    [Comment("Stores the available lamp style categories.")]
    public class LampStyle
    {
        [Comment("Primary key of the lamp style.")]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Comment("The name of the lamp style category.")]
        public string LampStyleName { get; set; } = null!;

        [Comment("Collection of Tiffany lamps that belong to this lamp style.")]
        public virtual IEnumerable<TiffanyLamp> tiffanyLamps { get; set; } = new List<TiffanyLamp>();
    }
}