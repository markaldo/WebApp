using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Entities;

namespace WebShop.Core.Repositories
{
    public interface ICategoryRepository
    {
        
        Task<IEnumerable<ProductCategory>> GetAllAsync();

        
        //Task<ProductCategory?> GetById(int id);
        //Task AddSync(ProductCategory category);
        //void UpdateSync(ProductCategory category);
        //void DeleteSync(ProductCategory category);
        //Task SaveChangeAsync();
    }
}
