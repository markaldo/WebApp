using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Security.Claims;
using System.Text.Json;
using WebShop.Core.Entities;
using WebShop.Core.Repositories;
using WebShop.Infra.DependencyInjection;
using WebShop.Infra.Identity;
using WebShop.Infra.Persistence;
using WebShop.MVC.Models;

namespace WebShop.MVC.Controllers
{
    public class ShopController : Controller
    {
        
        private readonly ILogger<ShopController> _logger;
        private readonly IShoppingCartService _cartService;
        private readonly IWishlistService _wishlistService;
        private readonly IOrderRepository _orderService;
        private readonly IAddress _addressService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShopController(UserManager<ApplicationUser> userManager, ILogger<ShopController> logger, IShoppingCartService cartService, IWishlistService wishlistService, IOrderRepository orderService, IAddress addressService)
        {
            _logger = logger;
            _cartService = cartService;
            _wishlistService = wishlistService;
            _orderService = orderService;
            _addressService = addressService;
            _userManager = userManager;
        }

        // [HttpGet]
        public IActionResult ProductDetails()
        {
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
            foreach (var item in items.Take(3)) 
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

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var cartItems = await _cartService.GetCartItemsAsync();
            if (!cartItems.Any()) return RedirectToAction("Cart");

            ViewBag.OrderTotal = await _cartService.GetTotalAsync();
            ViewBag.CartItems = cartItems;

            var model = new CheckoutViewModel
            {
                OrderTotal = (decimal)ViewBag.OrderTotal,
                ItemCount = cartItems.Sum(i => i.Quantity)
            };

            if (User.Identity?.IsAuthenticated == true)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user != null)
                    {
                        model.Email = user.Email ?? string.Empty;
                        model.FirstName = user.FirstName ?? string.Empty;
                        model.LastName = user.LastName ?? string.Empty;
                        model.Phone = user.PhoneNumber ?? string.Empty;
                    }

                    var userAddresses = await _addressService.GetUserAddressesAsync(userId);
                    var defaultAddress = userAddresses.FirstOrDefault(a => a.IsDefault);

                    if (defaultAddress != null)
                    {
                        model.HasSavedAddress = true;
                        model.AddressLine1 = defaultAddress.AddressLine1;
                        model.AddressLine2 = defaultAddress.AddressLine2 ?? string.Empty;
                        model.City = defaultAddress.City;
                        model.PostalCode = defaultAddress.PostalCode;
                        model.Country = defaultAddress.Country;
                        model.AdditionalInfo = defaultAddress.AdditionalInfo;
                    }
                }
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var cart_Items = await _cartService.GetCartItemsAsync();
                ViewBag.Total = await _cartService.GetTotalAsync();
                ViewBag.CartItems = cart_Items;
                return View(model);
            }

            var cartItems = await _cartService.GetCartItemsAsync();
            var total = await _cartService.GetTotalAsync();

            string? userId = User?.Identity?.IsAuthenticated == true
                ? User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                : null;

            if (userId == null)
            {
                var guestAddress = new GuestAddressDto
                {
                    AddressLine1 = model.AddressLine1,
                    AddressLine2 = model.AddressLine2,
                    City = model.City,
                    PostalCode = model.PostalCode,
                    Country = model.Country,
                    Phone = model.Phone,
                    AdditionalInfo = model.AdditionalInfo ?? string.Empty
                };

                var json = JsonSerializer.Serialize(guestAddress);

                Response.Cookies.Append("GuestAddress", json, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddHours(1), 
                    HttpOnly = true,
                    Secure = Request.IsHttps,
                    IsEssential = true
                });
            }
            else
            {
                var userAddresses = await _addressService.GetUserAddressesAsync(userId);
                var defaultAddress = userAddresses.FirstOrDefault(a => a.IsDefault);

                if (defaultAddress == null)
                {
                    var address = CreateAddressFromModel(model, userId);
                    address.IsDefault = true;
                    await _addressService.SaveAddressAsync(address);
                }
                else if (model.SaveAddress && !model.HasSavedAddress)
                {
                    var newAddress = CreateAddressFromModel(model, userId);
                    newAddress.IsDefault = false;
                    await _addressService.SaveAddressAsync(newAddress);
                }
            }

            var order = new Order
            {
                TotalPrice = total,
                OrderDate = DateTime.UtcNow,
                UserId = userId,
                AdditionalInfo = model.Phone ?? "",
                OrderLines = new List<OrderLine>()
            };

            var orderId = await _orderService.CreateOrderAsync(order, cartItems);
            await _cartService.ClearCartAsync();

            return RedirectToAction("OrderConfirmation", new { orderId });
        }

        private Address CreateAddressFromModel(CheckoutViewModel model, string userId)
        {
            return new Address
            {
                UserId = userId,
                LocationType = model.AddressName ?? "Home",
                AddressLine1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2,
                City = model.City,
                PostalCode = model.PostalCode,
                Country = model.Country,
                AdditionalInfo = model.AdditionalInfo ?? string.Empty,
                IsDefault = true
            };
        }

        private string GetGuestUserIdFromCookie()
        {
            if (Request.Cookies.TryGetValue("GuestUserId", out var guestId))
                return guestId;

            var newGuestId = Guid.NewGuid().ToString("N")[..8];
            Response.Cookies.Append("GuestUserId", newGuestId, new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(30),
                HttpOnly = true,
                Secure = Request.IsHttps
            });
            return newGuestId;
        }

        private async Task<List<SelectListItem>> LoadUserAddresses(string userId)
        {
            var addresses = await _addressService.GetUserAddressesAsync(userId);
            return addresses.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = $"{a.AddressLine1} - {a.City}, {a.PostalCode}",
                Selected = a.IsDefault
            }).ToList();
        }

        [HttpGet]
        public async Task<IActionResult> OrderConfirmation(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null) return NotFound();

            return View(order);  
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null) return NotFound();

            return Json(new
            {
                orderNumber = order.Id.ToString(),  
                totalAmount = order.TotalPrice.ToString("C"),
                orderDate = order.OrderDate.ToString("dd MMM yyyy HH:mm")
            });
        }

        [HttpGet]
        public async Task<IActionResult> Invoice(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null) return NotFound();

            var model = new InvoiceViewModel { Order = order };

            if (!string.IsNullOrEmpty(order.UserId))
            {
                var addresses = await _addressService.GetUserAddressesAsync(order.UserId);
                model.Address = addresses.FirstOrDefault(a => a.IsDefault);
            }
            else
            {
                if (Request.Cookies.TryGetValue("GuestAddress", out var guestJson))
                {
                    try
                    {
                        var guestAddress = JsonSerializer.Deserialize<GuestAddressDto>(guestJson);
                        model.GuestAddress = guestAddress;
                        Response.Cookies.Delete("GuestAddress");
                    }
                    catch
                    {
                        
                    }
                }
            }

            return View(model);
        }

    }
}
