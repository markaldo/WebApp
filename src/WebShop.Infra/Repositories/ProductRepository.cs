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

        public async Task AddSync(Product product)
        => await _context.Products.AddAsync(product);

        public void UpdateSync(Product product)
        => _context.Products.Update(product);

        public void DeleteSync(Product product)
        => _context.Products.Remove(product);

        public async Task SaveChangeAsync()
        => await _context.SaveChangesAsync();
    }
}
