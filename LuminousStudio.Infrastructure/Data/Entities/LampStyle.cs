namespace LuminousStudio.Infrastructure.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore;

    [Comment("Stores the available lamp style categories.")]
    public class LampStyle
    {
        [Comment("Primary key of the lamp style.")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(30)]
        [Comment("The name of the lamp style category.")]
        public string LampStyleName { get; set; } = null!;

        [Comment("Collection of Tiffany lamps that belong to this lamp style.")]
        public virtual ICollection<TiffanyLamp> TiffanyLamps { get; set; } = new List<TiffanyLamp>();
    }
}