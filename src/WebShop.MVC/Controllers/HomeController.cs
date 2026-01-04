using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebShop.Core.Repositories;
using WebShop.MVC.Models;

namespace WebShop.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartService _cartService;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository, IShoppingCartService cartService, ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _cartService = cartService;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index(int? CategoryId, decimal? minPrice, decimal? maxPrice)
        {
            var categories = (await _categoryRepository.GetAllAsync()).Select(c => new CategoryViewModel
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            });

            var productsSource = CategoryId.HasValue
                ? await _productRepository.GetAllByCategoryIdAsync(CategoryId.Value)
                : await _productRepository.GetAllAsync();
            if (minPrice.HasValue)
            {
                productsSource = productsSource.Where(p => p.Price >= minPrice);
            }
            if (maxPrice.HasValue)
            {
                productsSource = productsSource.Where(p => p.Price <= maxPrice);
            }

            var products = new List<ProductViewModel>();
            foreach (var p in productsSource)
            {
                var badge = await _productRepository.GetBadgeAsync(p);

                products.Add(new ProductViewModel
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    SalePrice = p.SalePrice,
                    ImageUrl = p.ImageUrl,
                    CategoryId = p.ProductCategoryId,
                    CategoryName = p.Category.CategoryName,
                    Badge = badge
                });
            }

            var viewModel = new HomeViewModel()
            {
                Products = products,
                Categories = categories,
                SelectedCategoryId = CategoryId,
                MinPrice = minPrice,
                MaxPrice = maxPrice
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult HeaderCartFragment()
        {
            return ViewComponent("CartHeader");
        }
    }
}
