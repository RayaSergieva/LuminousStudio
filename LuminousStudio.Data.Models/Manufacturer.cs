namespace LuminousStudio.Data.Models
{
    public class Manufacturer
    {
        public Guid Id { get; set; }

        public string ManufacturerName { get; set; } = null!;

        public virtual ICollection<TiffanyLamp> TiffanyLamps { get; set; } = new List<TiffanyLamp>();
    }
}