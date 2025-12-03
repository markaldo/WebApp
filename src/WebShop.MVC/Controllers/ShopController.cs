using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebShop.MVC.Controllers
{
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;
        // TODO: inject services as needed, e.g. IProductService, ICartService, IOrderService
        // private readonly IProductService _products;
        // private readonly ICartService _cart;
        public ShopController(ILogger<ShopController> logger /*, IProductService products, ICartService cart */)
        {
            _logger = logger;
            // _products = products;
            // _cart = cart;
        }

        // Optional landing page -> Views/Shop/Index.cshtml (e.g., product list)
        [HttpGet]
        public IActionResult Index()
        {
            // var model = _products.GetFeaturedOrAll();
            return View();
        }

        // GET: /Shop/Product/123 or /Shop/Product?id=123
        // Maps to Views/Shop/Product.cshtml (from shop-product-full.html)
        [HttpGet]
        public IActionResult Product(int? id)
        {
            if (id == null)
            {
                // No id supplied; you can redirect to Index or show a not-found message
                return RedirectToAction(nameof(Index));
            }

            // var product = _products.GetById(id.Value);
            // if (product == null) return NotFound();
            // return View(product);

            return View();
        }

        // GET: /Shop/Cart
        // Maps to Views/Shop/Cart.cshtml (from shop-cart.html)
        [HttpGet]
        public IActionResult Cart()
        {
            // var cart = _cart.GetForUser(User);
            // return View(cart);
            return View();
        }

        // POST: /Shop/Cart/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            // if (quantity < 1) quantity = 1;
            // _cart.Add(User, productId, quantity);
            return RedirectToAction(nameof(Cart));
        }

        // POST: /Shop/Cart/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCart(int productId, int quantity)
        {
            // _cart.Update(User, productId, quantity);
            return RedirectToAction(nameof(Cart));
        }

        // POST: /Shop/Cart/Remove
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int productId)
        {
            // _cart.Remove(User, productId);
            return RedirectToAction(nameof(Cart));
        }

        // GET: /Shop/Wishlist
        // Maps to Views/Shop/Wishlist.cshtml (from shop-wishlist.html)
        [HttpGet]
        public IActionResult Wishlist()
        {
            // var wishlist = _products.GetWishlist(User);
            // return View(wishlist);
            return View();
        }

        // POST: /Shop/Wishlist/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToWishlist(int productId)
        {
            // _products.AddToWishlist(User, productId);
            return RedirectToAction(nameof(Wishlist));
        }

        // POST: /Shop/Wishlist/Remove
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromWishlist(int productId)
        {
            // _products.RemoveFromWishlist(User, productId);
            return RedirectToAction(nameof(Wishlist));
        }

        // GET: /Shop/Checkout
        // Maps to Views/Shop/Checkout.cshtml (from shop-checkout.html)
        [HttpGet]
        public IActionResult Checkout()
        {
            // var model = _cart.GetCheckoutModel(User);
            // return View(model);
            return View();
        }

        // POST: /Shop/Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult Checkout(/* CheckoutViewModel model */)
        /*{
            // if (!ModelState.IsValid) return View(model);
            // var orderId = _cart.PlaceOrder(User, model);
            // return RedirectToAction(nameof(OrderConfirmation), new { id = orderId });
            return RedirectToAction(nameof(OrderConfirmation), new { id = 0 });
        }*/

        // GET: /Shop/OrderConfirmation/1001
        [HttpGet]
        public IActionResult OrderConfirmation(int id)
        {
            // var order = _orders.GetByIdForUser(User, id);
            // if (order == null) return NotFound();
            // return View(order);
            return View();
        }
    }
}
