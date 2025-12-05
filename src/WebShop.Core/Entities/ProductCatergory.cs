namespace WebShop.Core.Entities
{
    public class ProductCategory
    {
        public int CatergoryId { get; set; }          
        public string CatergoryName { get; set; }   
        public string? IconUrl { get; set; }

        // Navigation
        public IEnumerable<Product> Products { get; set; } = new List<Product>();

        // public string? UrlSlug { get; set; }    // "snack", "vegetables" (for URLs later)
    }
}
