using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Entities;

namespace WebShop.Core.Repositories
{
    public interface IWishlistService
    {
        Task<IEnumerable<WishlistItem>> GetWishlistAsync();
        Task AddToWishlistAsync(int productId);
        Task RemoveFromWishlistAsync(int productId);
        Task ClearWishlistAsync();
        Task<bool> IsInWishlistAsync(int productId);
    }
}
