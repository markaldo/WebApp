using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Entities;

namespace WebShop.Core.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetById(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetAllByCategoryIdAsync(int? categoryid);
        Task AddSync(Product product);
        void UpdateSync(Product product);
        void DeleteSync(Product product);
        Task SaveChangeAsync();
    }
}
