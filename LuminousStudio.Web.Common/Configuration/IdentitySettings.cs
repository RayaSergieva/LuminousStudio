namespace LuminousStudio.Web.Common.Configuration
{
    public class IdentitySettings
    {
        public bool RequireConfirmedAccount { get; set; }
        public bool RequireDigit { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireUppercase { get; set; }
        public bool RequireNonAlphanumeric { get; set; }
        public int RequiredLength { get; set; }
    }
}
