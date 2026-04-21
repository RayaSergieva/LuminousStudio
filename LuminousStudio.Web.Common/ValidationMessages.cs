namespace LuminousStudio.Web.Common
{
    public static class ValidationMessages
    {
        public static class TiffanyLamp
        {
            public const string NameRequiredMessage = "Lamp name is required.";
            public const string NameMinLengthMessage = "Lamp name must be at least 2 characters.";
            public const string NameMaxLengthMessage = "Lamp name cannot exceed 30 characters.";

            public const string ManufacturerRequiredMessage = "Manufacturer is required.";
            public const string LampStyleRequiredMessage = "Lamp style is required.";

            public const string PictureRequiredMessage = "Picture URL is required.";

            public const string QuantityRangeMessage = "Quantity must be between 0 and 5000.";
            public const string PriceRangeMessage = "Price must be between 0.01 and 1000000.";
            public const string DiscountRangeMessage = "Discount must be between 0 and 100.";
        }

        public static class Order
        {
            public const string QuantityRangeMessage = "Quantity must be between 1 and 1000.";
            public const string PriceRangeMessage = "Price must be between 0.01 and 1000000.";
            public const string DiscountRangeMessage = "Discount must be between 0 and 100.";
        }

        public static class ApplicationUser
        {
            public const string FirstNameRequiredMessage = "First name is required.";
            public const string FirstNameMinLengthMessage = "First name must be at least 2 characters.";
            public const string FirstNameMaxLengthMessage = "First name cannot exceed 30 characters.";

            public const string LastNameRequiredMessage = "Last name is required.";
            public const string LastNameMinLengthMessage = "Last name must be at least 2 characters.";
            public const string LastNameMaxLengthMessage = "Last name cannot exceed 30 characters.";

            public const string AddressRequiredMessage = "Address is required.";
            public const string AddressMinLengthMessage = "Address must be at least 5 characters.";
            public const string AddressMaxLengthMessage = "Address cannot exceed 50 characters.";

            public const string UsernameRequiredMessage = "Username is required.";
            public const string EmailRequiredMessage = "Email is required.";
            public const string EmailInvalidMessage = "Please enter a valid email address.";

            public const string PasswordRequiredMessage = "Password is required.";
            public const string PasswordMinLengthMessage = "Password must be at least 5 characters.";
            public const string PasswordMaxLengthMessage = "Password cannot exceed 100 characters.";
            public const string PasswordMismatchMessage = "Passwords do not match.";
        }

        public static class ShoppingCart
        {
            public const string CountRangeMessage = "Count must be between 1 and 1000.";
            public const string PriceRangeMessage = "Price must be between 0.01 and 1000000.";
        }
    }
}
