using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebShop.Core.Entities;
using WebShop.Core.Repositories;

namespace WebShop.Infra.Services
{
    public class WishlistService : IWishlistService
    {
        private const string WISHLIST_ITEMS_KEY = "WebShop.WishlistItems";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductRepository _productRepository;

        public WishlistService(IHttpContextAccessor httpContextAccessor, IProductRepository productRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<WishlistItem>> GetWishlistAsync()
        {
            var dict = GetWishlistIds();
            if (!dict.Any()) return Enumerable.Empty<WishlistItem>();

            var products = await _productRepository.GetAllAsync();
            return dict.Keys
                .Select(id => products.FirstOrDefault(p => p.Id == id))
                .Where(p => p != null)
                .Select(p => new WishlistItem
                {
                    ProductId = p.Id,
                    ProductName = p.ProductName,
                    ImageUrl = p.ImageUrl ?? "",
                    Price = p.Price
                });
        }

        public async Task AddToWishlistAsync(int productId)
        {
            var dict = GetWishlistIds();
            dict[productId] = true;    
            SaveWishlistIds(dict);
        }

        public async Task RemoveFromWishlistAsync(int productId)
        {
            var dict = GetWishlistIds();
            if (dict.Remove(productId))
            {
                SaveWishlistIds(dict);
            }
        }

        public Task ClearWishlistAsync()
        {
            var context = _httpContextAccessor.HttpContext;
            context.Response.Cookies.Delete(WISHLIST_ITEMS_KEY);
            return Task.CompletedTask;
        }

        public Task<bool> IsInWishlistAsync(int productId)
        {
            var dict = GetWishlistIds();
            return Task.FromResult(dict.ContainsKey(productId));
        }

        private Dictionary<int, bool> GetWishlistIds()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context.Request.Cookies.TryGetValue(WISHLIST_ITEMS_KEY, out var cookieValue))
            {
                try
                {
                    return JsonSerializer.Deserialize<Dictionary<int, bool>>(cookieValue)
                           ?? new Dictionary<int, bool>();
                }
                catch
                {
                    return new Dictionary<int, bool>();
                }
            }
            return new Dictionary<int, bool>();
        }

        private void SaveWishlistIds(Dictionary<int, bool> ids)
        {
            var context = _httpContextAccessor.HttpContext;
            var json = JsonSerializer.Serialize(ids);

            context.Response.Cookies.Append(WISHLIST_ITEMS_KEY, json, new CookieOptions
            {
                HttpOnly = false,
                Expires = DateTime.UtcNow.AddDays(30),
                Secure = true,
                SameSite = SameSiteMode.Strict
            });
        }
    }
}
