namespace WebShop.MVC.Models
{
    public class HomeViewModel
    {
        public required IEnumerable<ProductViewModel> Products { get; set; }
        public required IEnumerable<CategoryViewModel> Categories { get; set; }
        public int CartItemCount { get; set; } = 0;
        public int? SelectedCategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
