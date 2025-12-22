using WebShop.Core.Entities;

namespace WebShop.MVC.Models
{
    public class HomeViewModel
    {
        public required IEnumerable<ProductViewModel> Products { get; set; }
        public required IEnumerable<CategoryViewModel> Categories { get; set; }
        public int CartItemCount { get; set; } = 0;
    }
}
