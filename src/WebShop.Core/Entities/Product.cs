namespace WebShop.Core.Entities

{   
    public class Product
    {
        public required int Id { get; set; }           
        public required string ProductName { get; set; } 
        public decimal Price { get; set; }
        public decimal? SalePrice { get; set; }
        public string ImageUrl { get; set; }
        public Badge Badge { get; set; } = Badge.None;
        public DateTime CreateUtc { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductCategory Category { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    }
    public enum Badge
    {
        None = 0,
        FreeShipping = 1,
        Sale = 2,
        New = 3,
        Hot = 4
    }
}


