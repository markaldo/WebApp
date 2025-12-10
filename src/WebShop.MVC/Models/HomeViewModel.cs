using WebShop.Core.Entities;

namespace WebShop.MVC.Models
{
    public class HomeViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }  
        public IEnumerable<ProductViewModel> Products { get; set; }

    }
}
