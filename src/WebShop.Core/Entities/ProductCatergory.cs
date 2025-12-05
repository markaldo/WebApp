namespace WebShop.MVC.Models.Entities
{
    public class ProductCategory
    {
        public int CatergoryId { get; set; }          // PK
        public string CatergoryName { get; set; } = null!;   // e.g. "Snack", "Vegetables" ,"Beverages"
        // public string? UrlSlug { get; set; }    // "snack", "vegetables" (for URLs later)
        // public string? IconPath { get; set; } // e.g. "assets/imgs/theme/icons/category-1.svg"

        // Navigation
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
