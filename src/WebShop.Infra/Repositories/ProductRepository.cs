using Microsoft.EntityFrameworkCore;
using WebShop.Core.Entities;
using WebShop.Core.Repositories;
using WebShop.Infra.Persistence;

namespace WebShop.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Product?> GetById(int id)
         => await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Product>> GetAllAsync()
        => await _context.Products.Include(p => p.Category).ToListAsync();

        public async Task<IEnumerable<Product>> GetAllByCategoryIdAsync(int? categoryid)
        => await _context.Products.Include(p => p.Category)
            .Where(p => p.ProductCategoryId == categoryid).ToListAsync();
        public async Task AddSync(Product product)
       => await _context.Products.AddAsync(product);

        public void UpdateSync(Product product)
        => _context.Products.Update(product);

        public void DeleteSync(Product product)
        => _context.Products.Remove(product);

        public async Task SaveChangeAsync()
        => await _context.SaveChangesAsync();
        public async Task<Badge> GetBadgeAsync(Product product)
        {
            if (product.Badge != Badge.None)
            {
                return product.Badge;
            }
            if ((DateTime.UtcNow - product.CreateUtc).TotalDays < 5)
            {
                return Badge.New;
            }

            var threeDaysAgo = DateTime.UtcNow.AddDays(-3);
            var totalSold = await _context.OrderLines
                .Where(ol => ol.ProductId == product.Id && ol.Order.OrderDate >= threeDaysAgo)
                .SumAsync(ol => ol.Quantity);

            if (totalSold > 50)
            {
                return Badge.Hot;
            }
            return Badge.None;
        }
    }
}
