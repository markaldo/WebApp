namespace WebShop.MVC.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }                 // PK
        public string ProductName { get; set; } = null!;   // "Seeds of Change Organic Quinoe"
        public decimal Price { get; set; }         // 28.85

        // Category
        public int ProductCategoryId { get; set; }
        public ProductCategory Category { get; set; } = null!;

        /* public string? UrlSlug { get; set; }          // "seeds-of-change-organic-quinoa"

        // Brand / vendor
        public string? Brand { get; set; }         // "NestFood", "StarKist", etc.

        // Pricing
    
        public decimal? OldPrice { get; set; }     // 32.80 if on sale
        public bool IsOnSale => OldPrice.HasValue && OldPrice > Price;

        // Rating
        public double Rating { get; set; }         // 4.0
        public int RatingCount { get; set; }       // optional, number of reviews

        // Images
        public string ImageUrl { get; set; } = null!;        // "assets/imgs/shop/product-1-1.jpg"
        public string? HoverImageUrl { get; set; }           // "assets/imgs/shop/product-1-2.jpg"

        // Badges shown on cards ("Hot", "Sale", "New", "-14%")
        public string? BadgeText { get; set; }     // e.g. "Hot", "Sale", "New", "-14%"
        public string? BadgeCssClass { get; set; } // e.g. "hot", "sale", "new", "best"

        // Basic stock / flags
        public bool IsFeatured { get; set; }       // show on home tabs / sliders
        public bool IsActive { get; set; } = true; */
    }

}
