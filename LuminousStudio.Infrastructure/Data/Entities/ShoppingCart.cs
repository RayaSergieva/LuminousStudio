using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuminousStudio.Infrastructure.Data.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public int TiffanyLampId { get; set; }

        [ForeignKey(nameof(TiffanyLampId))]
        public virtual TiffanyLamp TiffanyLamp { get; set; } = null!;

        [Range(1, 1000)]
        public int Count { get; set; }

        public string ApplicationUserId { get; set; } = null!;

        [ForeignKey(nameof(ApplicationUserId))]

        public decimal Price { get; set; }
    }
}
