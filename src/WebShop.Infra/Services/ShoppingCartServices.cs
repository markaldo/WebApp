using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebShop.Core.Entities;
using WebShop.Core.Repositories;

namespace WebShop.Infra
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductRepository _productRepository;
        private const string CART_KEY = "WebShop.CartId";
        private const string CART_ITEMS_KEY = "WebShop.CartItems";

        public ShoppingCartService(IHttpContextAccessor httpContextAccessor, IProductRepository productRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
        }

        public string GetCartId()
        {
            var context = _httpContextAccessor.HttpContext;

            if (context.Request.Cookies.TryGetValue(CART_KEY, out string cartId))
                return cartId; 

            var newCartId = Guid.NewGuid().ToString();
            context.Response.Cookies.Append(CART_KEY, newCartId, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(30),
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return newCartId;
        }

        public async Task AddToCartAsync(int productId, int quantity = 1)
        {
            var cartItems = await GetCartItemsDictionaryAsync();
            cartItems[productId] = cartItems.GetValueOrDefault(productId, 0) + quantity;
            await SaveCartItemsAsync(cartItems);
        }

        public async Task UpdateQuantityAsync(int productId, int quantity)
        {
            var cartItems = await GetCartItemsDictionaryAsync();
            if (quantity <= 0)
                cartItems.Remove(productId);
            else
                cartItems[productId] = quantity;
            await SaveCartItemsAsync(cartItems);
        }

        public async Task RemoveFromCartAsync(int productId)
        {
            var cartItems = await GetCartItemsDictionaryAsync();
            cartItems.Remove(productId);
            await SaveCartItemsAsync(cartItems);
        }

        public Task ClearCartAsync()
        {
            var context = _httpContextAccessor.HttpContext;
            context.Response.Cookies.Delete(CART_ITEMS_KEY);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsAsync()
        {
            var cartItemsDict = await GetCartItemsDictionaryAsync();
            var products = await _productRepository.GetAllAsync();

            var cartItems = new List<CartItem>();
            foreach (var kvp in cartItemsDict)
            {
                var product = products.FirstOrDefault(p => p.Id == kvp.Key);
                if (product != null)
                {
                    cartItems.Add(new CartItem
                    {
                        Product = product,
                        Quantity = kvp.Value
                    });
                }
            }
            return cartItems;
        }

        public async Task<decimal> GetTotalAsync()
        {
            var items = await GetCartItemsAsync();
            return items.Sum(item => item.LineTotal);
        }

        private Task<Dictionary<int, int>> GetCartItemsDictionaryAsync()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context.Request.Cookies.TryGetValue(CART_ITEMS_KEY, out string cookieValue))
            {
                try
                {
                    return Task.FromResult(
                        JsonSerializer.Deserialize<Dictionary<int, int>>(cookieValue)
                        ?? new Dictionary<int, int>()
                    );
                }
                catch
                {
                    return Task.FromResult(new Dictionary<int, int>());
                }
            }
            return Task.FromResult(new Dictionary<int, int>());
        }


        private Task SaveCartItemsAsync(Dictionary<int, int> cartItems)
        {
            var context = _httpContextAccessor.HttpContext;
            var json = JsonSerializer.Serialize(cartItems);

            context.Response.Cookies.Append(CART_ITEMS_KEY, json, new CookieOptions
            {
                HttpOnly = false, 
                Expires = DateTime.UtcNow.AddDays(30),
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return Task.CompletedTask;  
        }
    }
}
