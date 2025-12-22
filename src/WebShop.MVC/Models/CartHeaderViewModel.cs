namespace WebShop.MVC.Models
{
    public class CartHeaderViewModel
    {
        public int ItemCount { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<CartHeaderItemViewModel> Items { get; set; } = Enumerable.Empty<CartHeaderItemViewModel>();
    }
}
