namespace WebShop.Core.Entities
{
    public class ProductCategory
    {
        public int CatergoryId { get; set; }          // PK
        public string CatergoryName { get; set; } = null!;   // e.g. "Snack", "Vegetables" ,"Beverages
        public string? IconUrl { get; set; } // e.g. "assets/imgs/theme/icons/category-1.svg"

        // Navigation
        public ICollection<Product> Products { get; set; } = new List<Product>();

        // public string? UrlSlug { get; set; }    // "snack", "vegetables" (for URLs later)
    }
}
