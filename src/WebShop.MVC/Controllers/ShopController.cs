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
        // TODO: IProductService, ICartService, IOrderService
        // private readonly IProductService _products;

        public ShopController(ILogger<ShopController> logger , IShoppingCartService cartService /*, IProductService products*/)
        {
            _logger = logger;
            _cartService = cartService;
            // _products = products;
        }

        // Optional landing page -> Views/Shop/Index.cshtml (e.g., product list)
        // [HttpGet]
        public IActionResult ProductDetails()
        {
            // var model = _products.GetFeaturedOrAll();
            return View();
        }

        //public async Task<IActionResult> AddToCart(int id, int quantity = 1)
        //{
        //    await _cartService.AddToCartAsync(id, quantity);
        //    TempData["SuccessMessage"] = $"{quantity} item(s) added to cart!";
        //    return RedirectToAction("Index", "Home");
        //}

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
            ViewBag.Total = await _cartService.GetTotalAsync();
            return View(items);
        }

        // POST: /Shop/Cart/Update
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        public IActionResult UpdateCart(int productId, int quantity)
        {
            // _cart.Update(User, productId, quantity);
            return RedirectToAction(nameof(Cart));
        }

        // POST: /Shop/Cart/Remove
        //[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int productId)
        {
            // _cart.Remove(User, productId);
            return RedirectToAction(nameof(Cart));
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


        // GET: /Shop/Wishlist
        // Maps to Views/Shop/Wishlist.cshtml (from shop-wishlist.html)
        // [HttpGet]
        public IActionResult Wishlist()
        {
            // var wishlist = _products.GetWishlist(User);
            // return View(wishlist);
            return View();
        }

        // POST: /Shop/Wishlist/Add
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        public IActionResult AddToWishlist(int productId)
        {
            // _products.AddToWishlist(User, productId);
            return RedirectToAction(nameof(Wishlist));
        }

        // POST: /Shop/Wishlist/Remove
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        public IActionResult RemoveFromWishlist(int productId)
        {
            // _products.RemoveFromWishlist(User, productId);
            return RedirectToAction(nameof(Wishlist));
        }

        // GET: /Shop/Checkout
        // Maps to Views/Shop/Checkout.cshtml (from shop-checkout.html)
        // [HttpGet]
        public IActionResult Checkout()
        {
            // var model = _cart.GetCheckoutModel(User);
            // return View(model);
            return View();
        }

        // [HttpGet]
        public IActionResult OrderConfirmation(int id)
        {
            // var order = _orders.GetByIdForUser(User, id);
            // if (order == null) return NotFound();
            // return View(order);
            return View();
        }
    }
}
