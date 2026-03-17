namespace LuminousStudio.Models.ShoppingCart
{
    using Infrastructure.Data.Entities;
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
            = new List<ShoppingCart>();

        public decimal CartTotal
        {
            get
            {
                return ShoppingCartList?.Sum(item => item.Price * item.Count) ?? 0;
            }
        }
    }
}
