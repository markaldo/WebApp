using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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



    }
}
