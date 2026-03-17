namespace LuminousStudio.Core.Contracts
{
    using LuminousStudio.Infrastructure.Data.Entities;
    public interface ILampStyleService
    {
        List<LampStyle> GetLampStyles();
        LampStyle GetLampStyleById(int lampStyleId);
        List<TiffanyLamp> GetProductsByLampStyle(int lampStyleId);
    }
}
