using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Entities;

namespace WebShop.Core.Repositories
{
    public interface IShoppingCartService
    {
        Task AddToCartAsync(int productId, int quantity);
        Task UpdateQuantityAsync(int productId, int quantity);
        Task RemoveFromCartAsync(int productId);
        Task ClearCartAsync();
        Task<IEnumerable<CartItem>> GetCartItemsAsync();
        Task<decimal> GetTotalAsync();
        string GetCartId();
    }
}
