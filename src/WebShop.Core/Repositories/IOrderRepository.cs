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
        Task<Order?> GetOrderByIdAsync(int orderId); // Get one order by Id
        Task<IEnumerable<Order>> GetAllAsync(); //Get all orders
        Task<int> CreateOrderAsync(Order order, IEnumerable<CartItem> cartItems);
        Task<IEnumerable<Order>> GetUserOrdersAsync(string userId);

    }
}
