using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebShop.Core.Entities;
using WebShop.Core.Repositories;
using WebShop.Infra.Persistence;
using WebShop.MVC.Models;

namespace WebShop.MVC.Controllers
{
    public class ShopController : Controller
    {
        
        private readonly ILogger<ShopController> _logger;
        private readonly IShoppingCartService _cartService;

        public ShopController(ILogger<ShopController> logger , IShoppingCartService cartService /*, IProductService products*/)
        {
            _logger = logger;
            _cartService = cartService;
            // _products = products;
        }

        // [HttpGet]
        public IActionResult ProductDetails()
        {
            // var model = _products.GetFeaturedOrAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id, int quantity = 1)
        {
            await _cartService.AddToCartAsync(id, quantity);
            var cartItems = await _cartService.GetCartItemsAsync();
            var totalItems = cartItems.Sum(item => item.Quantity);
            var totalPrice = await _cartService.GetTotalAsync();

            return Json(new
            {
                success = true,
                totalItems = totalItems,
                totalPrice = totalPrice.ToString("C")
            });
        }

        public async Task<IActionResult> Cart()
        {
            var items = await _cartService.GetCartItemsAsync();
            ViewBag.Subtotal = items.Sum(item => item.LineTotal);
            ViewBag.Total = await _cartService.GetTotalAsync();
            return View(items);
        }

        // POST: /Shop/Cart/Update
        // [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> UpdateCart(int productId, int quantity)
        {
            quantity = Math.Max(0, quantity);

            // Get fresh data from updated cart
            await _cartService.UpdateQuantityAsync(productId, quantity);
            var items = await _cartService.GetCartItemsAsync();
            var total = await _cartService.GetTotalAsync();
            var updatedItem = items.FirstOrDefault(i => i.Product.Id == productId);

            return Json(new
            {
                success = true,
                newQuantity = quantity,
                lineTotal = updatedItem?.LineTotal.ToString("C") ?? "£0.00",
                cartTotal = total.ToString("C"),
                itemCount = items.Sum(i => i.Quantity),  
                remainingItems = items.Count()
            });
        }

        // POST: /Shop/Cart/Remove
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            await _cartService.RemoveFromCartAsync(productId);

            var items = await _cartService.GetCartItemsAsync();
            var total = await _cartService.GetTotalAsync();

            return Json(new
            {
                success = true,
                cartTotal = total.ToString("C"),
                itemCount = items.Sum(i => i.Quantity),           
                remainingItems = items.Count(i => i.Quantity > 0) 
            });
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            await _cartService.ClearCartAsync();
            return RedirectToAction("Cart");
        }

        [HttpGet]
        public async Task<IActionResult> GetCartDropdown()
        {
            var items = await _cartService.GetCartItemsAsync();
            var total = await _cartService.GetTotalAsync();

            var dropdownHtml = GenerateCartDropdownHtml(items, total);
            return Json(new { html = dropdownHtml, count = items.Sum(i => i.Quantity) });
        }

        private string GenerateCartDropdownHtml(IEnumerable<CartItem> items, decimal total)
        {
            var html = "";
            foreach (var item in items.Take(3)) // Show max 3 items
            {
                html += $@"
            <li>
                <div class='shopping-cart-img'>
                    <a asp-controller='Shop' asp-action='Product' asp-route-id='{item.Product.Id}'>
                        <img alt='Nest' src='{item.Product.ImageUrl ?? "~/assets/imgs/shop/thumbnail-1.jpg"}' />
                    </a>
                </div>
                <div class='shopping-cart-title'>
                    <h4><a asp-controller='Shop' asp-action='Product' asp-route-id='{item.Product.Id}'>{item.Product.ProductName}</a></h4>
                    <h4><span>{item.Quantity} × </span>{item.Product.Price.ToString("C")}</h4>
                </div>
                <div class='shopping-cart-delete'>
                    <a href='#' onclick='removeFromCart({item.Product.Id}); return false;'>
                        <i class='fi-rs-cross-small'></i>
                    </a>
                </div>
            </li>";
            }

            if (!items.Any())
                html += "<li class='text-center py-3'><em>Cart is empty</em></li>";

            return html;
        }
    }
}
