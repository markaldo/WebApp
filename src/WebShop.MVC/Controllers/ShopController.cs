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
        private readonly IWishlistService _wishlistService;

        public ShopController(ILogger<ShopController> logger, IShoppingCartService cartService, IWishlistService wishlistService /*, IProductService products*/)
        {
            _logger = logger;
            _cartService = cartService;
            _wishlistService = wishlistService;
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
                // totalItems = totalItems,
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

            await _cartService.UpdateQuantityAsync(productId, quantity);
            var items = await _cartService.GetCartItemsAsync();
            var total = await _cartService.GetTotalAsync();
            var updatedItem = items.FirstOrDefault(i => i.Product.Id == productId);

            return Json(new
            {
                success = true,
                newQuantity = quantity,
                lineTotal = updatedItem?.LineTotal.ToString("C") ?? "£0.00",
                subTotal = total.ToString("C"),
                cartTotal = (total * (decimal)1.12).ToString("C"),
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
                subTotal = total.ToString("C"),
                cartTotal = (total * (decimal)1.12).ToString("C"),
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

        public IActionResult HeaderCartFragment()
        {
            return ViewComponent("CartHeader");
        }

        // GET: /Shop/Wishlist
        public async Task<IActionResult> Wishlist()
        {
            var items = await _wishlistService.GetWishlistAsync();
            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist(int productId)
        {
            await _wishlistService.AddToWishlistAsync(productId);
            return NoContent(); 
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromWishlist(int productId)
        {
            await _wishlistService.RemoveFromWishlistAsync(productId);
            var wishlist = await _wishlistService.GetWishlistAsync();
            return Json(new { success = true, count = wishlist.Count() });
        }

        [HttpPost]
        public async Task<IActionResult> MoveToCart(int productId)
        {
            await _cartService.AddToCartAsync(productId, 1);
            await _wishlistService.RemoveFromWishlistAsync(productId);
            Console.WriteLine(productId);
    
            var wishlist = await _wishlistService.GetWishlistAsync();
            var cartCount = await _cartService.GetCartItemsAsync(); 
            HeaderCartFragment();
    
            return Json(new { 
                success = true, 
                wishlistCount = wishlist.Count(),
                cartCount = cartCount.Count()
            });
        }

        [HttpPost]
        public async Task<IActionResult> MoveAllToCart()
        {
            var items = await _wishlistService.GetWishlistAsync();
            foreach (var item in items)
            {
                await _cartService.AddToCartAsync(item.ProductId, 1);
            }
            await _wishlistService.ClearWishlistAsync();
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public async Task<IActionResult> ToggleWishlist([FromBody] ToggleWishlistRequest request)
        {
            if (request.ProductId <= 0) return BadRequest();

            var isInWishlist = await _wishlistService.IsInWishlistAsync(request.ProductId);

            if (isInWishlist)
            {
                await _wishlistService.RemoveFromWishlistAsync(request.ProductId);
            }
            else
            {
                await _wishlistService.AddToWishlistAsync(request.ProductId);
            }

            return Ok();
        }

        public class ToggleWishlistRequest
        {
            public int ProductId { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> CheckWishlistItems([FromBody] CheckWishlistRequest request)
        {
            var productIds = request.ProductIds ?? new List<int>();
            var wishlistItems = await _wishlistService.GetWishlistAsync();
            var inWishlist = wishlistItems.Select(x => x.ProductId).ToList();

            return Json(new { inWishlist = inWishlist });
        }

        [HttpPost]
        public async Task<IActionResult> CheckWishlistItem([FromBody] ToggleWishlistRequest request)
        {
            var isInWishlist = await _wishlistService.IsInWishlistAsync(request.ProductId);
            return Json(new { isInWishlist = isInWishlist });
        }

        public class CheckWishlistRequest
        {
            public List<int> ProductIds { get; set; } = new();
        }

        [HttpPost]
        public async Task<IActionResult> GetWishlistCount()
        {
            var items = await _wishlistService.GetWishlistAsync();
            var count = items.Count();

            return Json(new { count });
        }

    }
}
