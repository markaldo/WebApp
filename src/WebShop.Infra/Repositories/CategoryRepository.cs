using Microsoft.EntityFrameworkCore;
using WebShop.Core.Entities;
using WebShop.Core.Repositories;
using WebShop.Infra.Persistence;



namespace WebShop.Infra.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<ProductCategory?> GetById(int id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task AddSync(ProductCategory category)
        {
            await _context.Categories.AddAsync(category);
        }

        public void UpdateSync(ProductCategory category)
        {
            _context.Categories.Update(category);
        }

        public void DeleteSync(ProductCategory category)
        {
            _context.Categories.Remove(category);
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
