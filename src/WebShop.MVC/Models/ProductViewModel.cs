using WebShop.Core.Entities;

namespace WebShop.MVC.Models
{
    public class ProductViewModel
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }
        public Badge Badge { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
