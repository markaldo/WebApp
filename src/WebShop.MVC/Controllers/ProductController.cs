using Microsoft.AspNetCore.Mvc;
using WebShop.Core.Repositories;
using WebShop.MVC.Models;

namespace WebShop.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _productRepository;

        public ProductController(ILogger<ProductController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            var badge = await _productRepository.GetBadgeAsync(product);
            var viewModel = new ProductViewModel
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                SalePrice = product.SalePrice,
                ImageUrl = product.ImageUrl,
                Badge = badge
            };
            return View(model: viewModel);
        }
    }
}
