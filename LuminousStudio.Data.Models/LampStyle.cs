namespace LuminousStudio.Data.Models
{
    public class LampStyle
    {
        public Guid Id { get; set; }

        public string LampStyleName { get; set; } = null!;

        public virtual ICollection<TiffanyLamp> TiffanyLamps { get; set; } = new List<TiffanyLamp>();
    }
}