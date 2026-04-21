namespace LuminousStudio.Data.Common
{
    public static class EntityConstants
    {
        public static class ApplicationUser
        {
            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 30;

            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 30;

            public const int AddressMinLength = 5;
            public const int AddressMaxLength = 50;

            public const int PasswordMinLength = 5;
            public const int PasswordMaxLength = 100;
        }

        public static class TiffanyLamp
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 30;

            public const int QuantityMin = 0;
            public const int QuantityMax = 5000;

            public const string PriceMin = "0.01";
            public const string PriceMax = "1000000";

            public const string DiscountMin = "0";
            public const string DiscountMax = "100";
        }

        public static class LampStyle
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 30;
        }

        public static class Manufacturer
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 30;
        }

        public static class Order
        {
            public const int QuantityMin = 1;
            public const int QuantityMax = 1000;

            public const string PriceMin = "0.01";
            public const string PriceMax = "1000000";

            public const string DiscountMin = "0";
            public const string DiscountMax = "100";
        }

        public static class ShoppingCart
        {
            public const int CountMin = 1;
            public const int CountMax = 1000;

            public const string PriceMin = "0.01";
            public const string PriceMax = "1000000";
        }
    }
}
