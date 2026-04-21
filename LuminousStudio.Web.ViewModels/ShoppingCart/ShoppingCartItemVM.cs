namespace LuminousStudio.Web.ViewModels.ShoppingCart
{
    public class ShoppingCartItemVM
    {
        public Guid Id { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string Picture { get; set; } = string.Empty;

        public int Count { get; set; }

        public decimal Price { get; set; }
    }
}