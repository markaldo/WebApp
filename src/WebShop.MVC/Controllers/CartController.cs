using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Repositories;

namespace WebShop.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly IShoppingCartService _cartService;

        public CartController(IShoppingCartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _cartService.GetCartItemsAsync();
            ViewBag.Total = await _cartService.GetTotalAsync();
            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int productId, int quantity = 1)
        {
            await _cartService.AddToCartAsync(productId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(int productId, int quantity)
        {
            await _cartService.UpdateQuantityAsync(productId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int productId)
        {
            await _cartService.RemoveFromCartAsync(productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Clear()
        {
            await _cartService.ClearCartAsync();
            return RedirectToAction("Index");
        }
    }
}
