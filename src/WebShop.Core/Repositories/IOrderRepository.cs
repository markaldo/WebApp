using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Entities;

namespace WebShop.Core.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(int id); // Get one order by Id
        Task<IEnumerable<Order>> GetAllAsync(); //Get all orders

        //Task AddAsync(Order order); // Add new order
        //void DeleteAsync(int id); // Delete order by ID
        //void UpdateAsync(Order order);// Update existing order

        //Task SaveChangesAsync();      // Comit changes to DataBase 


    }
}
