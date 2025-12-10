namespace WebShop.Core.Entities
{
    public class ProductCategory
    {
        public int CategoryId { get; set; }          
        public string CategoryName { get; set; }   
        public string? IconUrl { get; set; }

        // Navigation
        public IEnumerable<Product> Products { get; set; } = new List<Product>();

        // public string? UrlSlug { get; set; }    // "snack", "vegetables" (for URLs later)
    }
}
