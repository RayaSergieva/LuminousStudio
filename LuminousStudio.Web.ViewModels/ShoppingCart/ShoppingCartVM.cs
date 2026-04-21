namespace LuminousStudio.Web.ViewModels.ShoppingCart
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCartItemVM> ShoppingCartList { get; set; }
            = new List<ShoppingCartItemVM>();

        public decimal CartTotal
        {
            get
            {
                return ShoppingCartList?.Sum(item => item.Price * item.Count) ?? 0;
            }
        }
    }
}
